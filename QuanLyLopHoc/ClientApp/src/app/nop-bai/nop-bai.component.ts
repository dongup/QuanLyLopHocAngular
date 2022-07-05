import { HttpClient } from '@angular/common/http';
import { Component, Inject, INJECTOR, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LopHoc } from '../models/interfaces';
import { ResponseModel } from '../models/response-model';

@Component({
  selector: 'app-nop-bai',
  templateUrl: './nop-bai.component.html',
  styleUrls: ['./nop-bai.component.css']
})
export class NopBaiComponent implements OnInit {
  maSinhVien: string = '';
  lopHocId: number = 0;
  hoVaTen: string = '';
  lopHocs: LopHoc[] = [];

  constructor(private http: HttpClient
    , @Inject("BASE_URL") private baseUrl: string
    , private router: Router
    , private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadDataLopHoc()
  }

  nopBai() {
    this.router.navigate([`nop-bai/${this.lopHocId}/${this.maSinhVien}/${this.hoVaTen}`], { relativeTo: this.route });
  }

  loadDataLopHoc() {
    this.http.get<ResponseModel<LopHoc[]>>(`${this.baseUrl}lopHoc`)
      .subscribe(rspns => {
        this.lopHocs = rspns.result;
      }
        , error => console.error(error));
  }
}
