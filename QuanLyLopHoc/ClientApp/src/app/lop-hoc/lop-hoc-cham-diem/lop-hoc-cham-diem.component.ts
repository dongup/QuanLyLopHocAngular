import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  editorOptions = { theme: 'vs-dark', language: 'c' };

  constructor(private router: Router
    , private route: ActivatedRoute
    , private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    this.idLopHoc = Number(routeParams.get("idLopHoc"));

    this.loadDataLopHoc();
  }


  onClickBaiTap(idSinhVien: number) {
    let clickedSinhVien = this.sinhViens.find(x => x.id == idSinhVien);
    if (clickedSinhVien != undefined) {
      this.slctedSinhVien = clickedSinhVien;
    }
  }

  loadDataSinhVien() {
    this.http.get<ResponseModel<LopHoc>>(`${this.baseUrl}lopHoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.lopHoc = rspns.result;
      }
        , error => console.error(error));
  }

  loadDataLopHoc() {
    this.http.get<ResponseModel<LopHoc>>(`${this.baseUrl}lopHoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.lopHoc = rspns.result;
      }
        , error => console.error(error));
  }
}

