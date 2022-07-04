import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResponseModel } from '../models/response-model'
import { Route, Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LopHoc } from '../models/interfaces';

@Component({
  selector: 'app-lop-hoc',
  templateUrl: './lop-hoc.component.html'
})
export class LopHocComponent {
  public lopHocs: LopHoc[] = [];

  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private router: Router
    , private route: ActivatedRoute
    , private toastr: ToastrService)
  {
    this.loadData();
  }

  loadData() {
    this.http.get<ResponseModel<LopHoc[]>>(this.baseUrl + 'lophoc')
      .subscribe(rspns =>
        {
          this.lopHocs = rspns.result;
        }
        , error => console.error(error));
  }

  toAddPage() {
    this.router.navigate(['add'], { relativeTo: this.route });
  }

  xoaLopHoc(lopHoc: LopHoc) {
    if (!window.confirm(`Xóa lớp học ${lopHoc.tenLopHoc}?`)) return;

    this.http.delete(this.baseUrl + 'lophoc/' + lopHoc.id)
      .subscribe(rspns => {
        let res = rspns as ResponseModel<Object>;
        if (res.isSucceed) {
          this.toastr.success(res.result as string);
          this.loadData();
        }
      }
      , error => console.error(error));
  }

}

