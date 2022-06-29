import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LopHoc } from '../lop-hoc.component';
import { ResponseModel } from '../../models/response-model'

@Component({
  selector: 'app-lop-hoc-add',
  templateUrl: './lop-hoc-add.component.html',
  styleUrls: ['./lop-hoc-add.component.css']
})
export class LopHocAddComponent implements OnInit {
  newLopHoc: LopHoc = {
    id: 0,
    sinhViens: [],
    tenLopHoc: ''
  };

  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private router: Router
    , private route: ActivatedRoute
    , private formBuilder: FormBuilder) {
   
  }

  ngOnInit(): void {

  }

  goBack() {
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  submitAdd() {
    this.http.post(this.baseUrl + 'lophoc', this.newLopHoc)
      .subscribe(rspns => {
        this.goBack();
        console.log(rspns);
        //this.forecasts= rspns.result;
      }
      , error => console.error(error));
  }
}
