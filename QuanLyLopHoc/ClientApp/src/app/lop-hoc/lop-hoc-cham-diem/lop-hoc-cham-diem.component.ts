import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { __classPrivateFieldGet } from 'tslib';
import { BaiTap, LopHoc, SinhVien } from '../../models/interfaces';
import { ResponseModel } from '../../models/response-model';

@Component({
  selector: 'app-lop-hoc-cham-diem',
  templateUrl: './lop-hoc-cham-diem.component.html',
  styleUrls: ['./lop-hoc-cham-diem.component.css']
})
export class LopHocChamDiemComponent implements OnInit {
  idLopHoc: number = 0;

  lopHoc: LopHoc = {
      tenLopHoc: '',
      id: 0,
      sinhViens: [],
      baiTaps: []
  };

  sinhViens: SinhVien[] = [];
  slctedSinhVien: SinhVien = {
      id: 0,
      maSinhVien: '',
      hoVaTen: '',
      idLopHoc: 0,
      baiTaps: []
  }

  editorOptions = {
    theme: 'vs-light',
    language: 'c',
    minimap: {
      enabled: false
    }
  };

  constructor(private router: Router
    , private route: ActivatedRoute
    , private http: HttpClient
    , private toastr: ToastrService
    , @Inject('API_URL') private apiUrl: string
    , @Inject('BASE_URL') private baseUrl: string  ) {
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    this.idLopHoc = Number(routeParams.get("idLopHoc"));

    this.loadDataLopHoc();
    this.loadDataSinhVien();
  }

  downLoadExcel() {
    this.http.get<ResponseModel<string>>(`${this.apiUrl}cham-diem/excel/${this.idLopHoc}`)
      .subscribe(content => {
        let rspns = content as ResponseModel<string>;
        if (rspns.isSucceed) {
          //this.toastr.success(rspns.result);
          window.location.href = this.baseUrl + rspns.result;
        }

        if (!rspns.isSucceed) {
          this.toastr.error(rspns.message);
        }
      }
      , error => console.error(error));
  }

  luuDiemSinhVien() {
    let request: any = {
      idSinhVien: this.slctedSinhVien.id,
      tongDiem: this.slctedSinhVien.tongDiem,
      nhanXet: this.slctedSinhVien.nhanXet,
      diemSos: this.slctedSinhVien.baiTaps.map(x => {
        return {
          diem: x.diemCham,
          idBaiTap: x.id,
          nhanXet: x.nhanXet
        }
      }),
    };

    this.http.post(`${this.apiUrl}cham-diem`, request)
      .subscribe(content => {
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

  onChamDiemSv(baiTap: BaiTap) {
    let diem = baiTap.diem ?? 0;
    let diemCham = baiTap.diemCham ?? 0;

    if (diemCham > diem) {
      baiTap.diemCham = baiTap.diem;
      this.toastr.error("Điểm chấm không được vượt quá số điểm tối đa!");
    }
    this.sumDiem();
  }

  sumDiem() {
    let diemArr = this.slctedSinhVien.baiTaps.map(x => x.diemCham ?? 0);
    this.slctedSinhVien.tongDiem = diemArr.reduce((a, b) => a + b, 0);
  }

  choDiemToiDa(baiTap: BaiTap) {
    baiTap.diemCham = baiTap.diem;
    this.sumDiem();
  }

  copyCode(baiTap: BaiTap) {
    navigator.clipboard.writeText(baiTap.traLoi ?? "");
    baiTap.coppied = true;
  }

  onSelectSinhVien(idSinhVien: number) {
    let clickedSinhVien = this.sinhViens.find(x => x.id == idSinhVien);
    if (clickedSinhVien != undefined) {
      this.slctedSinhVien = clickedSinhVien;
    }
  }

  loadDataSinhVien() {
    this.http.get<ResponseModel<SinhVien[]>>(`${this.apiUrl}cham-diem/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.sinhViens = rspns.result;
        let findSv = this.sinhViens.find(x => x.id == this.slctedSinhVien.id);
        if (findSv != undefined) {
          this.slctedSinhVien = findSv;
        }

      }
        , error => console.error(error));
  }

  loadDataLopHoc() {
    this.http.get<ResponseModel<LopHoc>>(`${this.apiUrl}lopHoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.lopHoc = rspns.result;
      }
        , error => console.error(error));
  }
}

