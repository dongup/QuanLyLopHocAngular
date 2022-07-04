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
    , @Inject('BASE_URL') private baseUrl: string) {
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
      id: 0
    })
    this.reOrder();
    
  }

  removeBaiTap(idBaiTap: string) {
    this.lopHoc.baiTaps = this.lopHoc.baiTaps.filter(x => x.localId != idBaiTap);
    this.reOrder();
  }

  reOrder() {
    this.lopHoc.baiTaps.forEach((x, idx) => x.stt = idx + 1);
  }

  submitEdit() {
    this.http.put(`${this.baseUrl}lophoc/${this.idLopHoc}`, this.lopHoc)
      .subscribe((content) => {
        let res = content as ResponseModel<string>;
        if (res.isSucceed) {
          this.toastr.success(res.message);
        }

        if (!res.isSucceed) {
          this.toastr.error(res.message);
        }
      }
      , error => console.error(error));
  }

  excelUploaded(rspns: ResponseModel<UploadFileResult>) {
    let filePath: string = rspns.result.filePath;
    this.http.get(this.baseUrl + 'sinhvienimport/from-excel-file?filePath=' + filePath)
      .subscribe((content) => {
        let rspns = content as ResponseModel<SinhVien[]>;

        this.lopHoc.sinhViens = rspns.result;
        //this.goBack();
      }
      , error => console.error(error));
  }

  loadDetail() {
    this.http.get<ResponseModel<LopHoc>>(`${this.baseUrl}lophoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.lopHoc = rspns.result;
        this.lopHoc.baiTaps.forEach(x => x.localId = uuidv4());
      }
      , error => console.error(error));
  }

}
