import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LopHoc, SinhVien } from '../../models/interfaces';
import { ResponseModel } from '../../models/response-model'
import { ToastrService } from 'ngx-toastr';
import { UploadFileResult } from '../../upload/upload.component';
import { v4 } from 'uuid';

@Component({
  selector: 'app-lop-hoc-add',
  templateUrl: './lop-hoc-add.component.html',
  styleUrls: ['./lop-hoc-add.component.css']
})
export class LopHocAddComponent implements OnInit {
  newLopHoc: LopHoc = {
    id: 0,
    sinhViens: [],
    tenLopHoc: '',
    baiTaps: []
  };
  dbPath: string = '';

  constructor(private http: HttpClient
    , @Inject('API_URL') private apiUrl: string
    , private router: Router
    , private route: ActivatedRoute
    , private toastr: ToastrService) {
   
  }

  ngOnInit(): void {

  }

  addBaiTap() {
    this.newLopHoc.baiTaps.push({
      noiDung: '',
      tieuDe: '',
      stt: this.newLopHoc.baiTaps.length + 1,
      localId: v4(),
      id: 0
    })
  }

  removeBaiTap(idBaiTap: string) {
    this.newLopHoc.baiTaps = this.newLopHoc.baiTaps.filter(x => x.localId != idBaiTap);
  }

  excelUploaded(rspns: ResponseModel<UploadFileResult>) {
    let filePath: string = rspns.result.filePath;
    this.http.get(this.apiUrl + 'sinhvienimport/from-excel-file?filePath=' + filePath)
      .subscribe((content) => {
        let rspns = content as ResponseModel<SinhVien[]>;

        this.newLopHoc.sinhViens = rspns.result;
        //this.goBack();
      }
        , error => console.error(error));
  }

  goBack() {
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  submitAdd() {
    this.http.post(this.apiUrl + 'lophoc', this.newLopHoc)
      .subscribe(content => {
        let rspns = content as ResponseModel<string>;
        this.toastr.success(rspns.result);
        this.goBack();
      }
      , error => console.error(error));
  }
}
