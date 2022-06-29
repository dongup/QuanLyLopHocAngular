import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResponseModel } from '../response-model'

@Component({
  selector: 'app-lop-hoc',
  templateUrl: './lop-hoc.component.html'
})
export class LopHocComponent {
  public forecasts: LopHoc[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ResponseModel<LopHoc[]>>(baseUrl + 'lophoc')
    .subscribe(rspns => {
      this.forecasts = rspns.result;
    }, error => console.error(error));
  }
}

interface LopHoc {
  tenLopHoc: string;
  id: number;
  sinhViens: []
}
