import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ResponseModel } from '../models/response-model';
@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent implements OnInit {
  progress: number = 0;
  message: string = '';
  @Output() public onUploadFinished = new EventEmitter<ResponseModel<UploadFileResult>>();
  //file: HTMLInputElement = new Htm

  constructor(private http: HttpClient
    , @Inject('API_URL') private apiUrl: string
    , private router: Router
    , private route: ActivatedRoute
    , private toastr: ToastrService) { }

  ngOnInit() {

  }

  uploadBtnClick(fileInput: HTMLInputElement) {
    fileInput.value = '';
    fileInput.click();
  }

  uploadFile = (files: FileList | null) => {
    if (files == null) return;

    if (files.length === 0) {
      return;
    }

    let fileToUpload:File = files[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    
    this.http.post(this.apiUrl + 'upload', formData, {reportProgress: true, observe: 'events'})
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress) {
            this.progress = Math.round(100 * event.loaded / (event.total == null ? event.loaded : event.total));
            this.message = 'Uploading...';
          }

          if (event.type === HttpEventType.Response) {
            let rspns = <ResponseModel<UploadFileResult>>event.body;
            this.message = "Upload ok";
            this.onUploadFinished.emit(rspns);
          }
        },
        error: (err: HttpErrorResponse) => {
          console.error(err);
          this.message = err.message;
        }
    });
  }
}

export interface UploadFileResult {
  filePath: string,
  fileName: string
}
