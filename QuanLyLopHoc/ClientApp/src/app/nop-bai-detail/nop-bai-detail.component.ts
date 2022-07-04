import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BaiTap } from '../models/interfaces';
import { ResponseModel } from '../models/response-model';

@Component({
  selector: 'app-nop-bai-detail',
  templateUrl: './nop-bai-detail.component.html',
  styleUrls: ['./nop-bai-detail.component.css']
})
export class NopBaiDetailComponent implements OnInit {
  maSinhVien: string = '';
  idLopHoc: number = 0;
  baiTaps: BaiTap[] = [];
  slctedBaiTap: BaiTap = {
    id: 0,
    tieuDe: '',
    noiDung: '',
    stt: 0,
    localId: '',
    traLoi: `
      #include <stdio.h>

      int main() {
         printf("Hello, world!");
         return 0;
      }
    `,
  };

  editorOptions = { theme: 'vs-dark', language: 'c' };

  constructor(private router: Router
    , private route: ActivatedRoute
    , private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    this.maSinhVien = String(routeParams.get("maSinhVien"));
    this.idLopHoc = Number(routeParams.get("idLopHoc"));
    this.loadData();
  }

  nopBai() {
    this.http.post(`${this.baseUrl}executeCode`, {
      maSinhVien: this.maSinhVien,
      tenSinhVien: 'Nguyễn Văn Đông',
      idLopHoc: this.idLopHoc,
      idBaiTap: this.slctedBaiTap.id,
      traLoi: this.slctedBaiTap.traLoi
    })
      .subscribe(content => {
        let rspns = content as ResponseModel<string>;
      }
        , error => console.error(error));
  }

  runCode() {
    this.http.post(`${this.baseUrl}executeCode`, {
      maSinhVien: this.maSinhVien,
      tenSinhVien: 'Nguyễn Văn Đông',
      idLopHoc: this.idLopHoc,
      idBaiTap: this.slctedBaiTap.id,
      traLoi: this.slctedBaiTap.traLoi
    })
      .subscribe(content => {
        let rspns = content as ResponseModel<string>;
      }
      , error => console.error(error));
  }

  onClickBaiTap(idBaiTap: number) {
    let clickBaiTap = this.baiTaps.find(x => x.id == idBaiTap);
    if (clickBaiTap != undefined) {
      this.slctedBaiTap = clickBaiTap;
    }
  }

  loadData() {
    this.http.get<ResponseModel<BaiTap[]>>(`${this.baseUrl}baiTap/by-lop-hoc/${this.idLopHoc}`)
      .subscribe(rspns => {
        this.baiTaps = rspns.result;
        this.slctedBaiTap = this.baiTaps[0];
      }
        , error => console.error(error));
  }
}

