import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { v4 as uuidv4 } from 'uuid';
import { LopHoc, SinhVien } from '../../models/interfaces';
import { ResponseModel } from '../../models/response-model';
import { UploadFileResult } from '../../upload/upload.component';

@Component({
  selector: 'app-lop-hoc-edit',
  templateUrl: './lop-hoc-edit.component.html',
  styleUrls: ['./lop-hoc-edit.component.css']
})
export class LopHocEditComponent implements OnInit {
  idLopHoc: number = 0;
  lopHoc: LopHoc = {
      tenLopHoc: '',
      id: 0,
      sinhViens: [],
      baiTaps: []
  }

  constructor(private router: Router
    , private route: ActivatedRoute
    , private http: HttpClient
    , private toastr: ToastrService
    , @Inject('API_URL') private apiUrl: string) {
  }

  ngOnInit(): void {
    const routeParam = this.route.snapshot.paramMap;
    this.idLopHoc = Number(routeParam.get("idLopHoc"));

    this.loadDetail();
  }

  addBaiTap() {
    this.lopHoc.baiTaps.push({
      noiDung: '',
      tieuDe: '',
      stt: this.lopHoc.baiTaps.length + 1,
      localId: uuidv4(),
      id: 0,
      diem: 0
    })
    this.reOrder();
    
  }

  removeBaiTap(idBaiTap: string) {
    this.lopHoc.baiTaps = this.lopHoc.baiTaps.filter(x => x.localId != idBaiTap);
    this.reOrder();
  }

  removeSinhVien(idSinhVien: number) {
    if (!window.confirm("Xóa sinh viên " + idSinhVien + "?")) return;

    this.http.delete(`${this.apiUrl}sinhVien/${idSinhVien}`)
      .subscribe((content) => {
        let res = content as ResponseModel<string>;
        if (res.isSucceed) {
          this.toastr.success(res.result);
          this.lopHoc.sinhViens = this.lopHoc.sinhViens.filter(x => x.id != idSinhVien);
        }

        if (!res.isSucceed) {
          this.toastr.error(res.message);
        }
      }
        , error => console.error(error));
  }

  reOrder() {
    this.lopHoc.baiTaps.forEach((x, idx) => x.stt = idx + 1);
  }

  submitEdit() {
    this.http.put(`${this.apiUrl}lophoc/${this.idLopHoc}`, this.lopHoc)
      .subscribe((content) => {
        let rspns = content as ResponseModel<string>;
        if (rspns.isSucceed) {
          this.toastr.success(rspns.result);
        }

        if (!rspns.isSucceed) {
          this.toastr.error(rspns.message);
        }
      }
      , error => console.error(error));
  }

  excelUploaded(rspns: ResponseModel<UploadFileResult>) {
    let filePath: string = rspns.result.filePath;
    this.http.post(this.apiUrl + 'import/sinh-vien', { filePath: filePath })
      .subscribe((content) => {
        let rspns = content as ResponseModel<SinhVien[]>;

        this.lopHoc.sinhViens = rspns.result;
        //this.goBack();
      }
      , error => console.error(error));
  }

  loadDetail() {
    this.http.get<ResponseModel<LopHoc>>(`${this.apiUrl}lophoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.lopHoc = rspns.result;
        this.lopHoc.baiTaps.forEach(x => x.localId = uuidv4());
      }
      , error => console.error(error));
  }

}
