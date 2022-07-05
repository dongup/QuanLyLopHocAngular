import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BaiTap, SinhVienTraLoi } from '../models/interfaces';
import { ResponseModel } from '../models/response-model';

@Component({
  selector: 'app-nop-bai-detail',
  templateUrl: './nop-bai-detail.component.html',
  styleUrls: ['./nop-bai-detail.component.css']
})
export class NopBaiDetailComponent implements OnInit {
  maSinhVien: string = '';
  hoVaTen: string = '';
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
    , private toastr: ToastrService
    , @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    this.maSinhVien = String(routeParams.get("maSinhVien"));
    this.idLopHoc = Number(routeParams.get("idLopHoc"));
    this.hoVaTen = String(routeParams.get("hoVaTen"));
    this.loadData();
  }

  nopBai() {
    let request: any = {
      maSinhVien: this.maSinhVien,
      tenSinhVien: this.hoVaTen,
      idLopHoc: this.idLopHoc,
      traLois: this.baiTaps.map(x => {
        return {
          idBaiTap: x.id,
          traLoi: x.traLoi
        }
      })
    }

    this.http.post(`${this.baseUrl}nop-bai`, request)
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

  runCode() {
    this.http.post(`${this.baseUrl}executeCode`, {
      maSinhVien: this.maSinhVien,
      tenSinhVien: 'Nguyễn Văn Đông',
      idLopHoc: this.idLopHoc,
      idBaiTap: this.slctedBaiTap.id,
      traLoi: this.slctedBaiTap.traLoi
    })
      .subscribe(content => {
        let res = content as ResponseModel<string>;
        if (res.isSucceed) {
          this.toastr.success(res.result);
        }

        if (!res.isSucceed) {
          this.toastr.error(res.message);
        }
      }
        , error => this.toastr.error(error.message));
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
        if (rspns.isSucceed) {
          this.baiTaps = rspns.result;
          this.slctedBaiTap = this.baiTaps[0];
        }

        if (!rspns.isSucceed) {
          this.toastr.error(rspns.message);
        }
      }
        , error => console.error(error));
  }

}

