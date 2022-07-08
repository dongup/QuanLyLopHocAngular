import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { LopHocComponent } from './lop-hoc/lop-hoc.component';
import { LopHocAddComponent } from './lop-hoc/lop-hoc-add/lop-hoc-add.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ToastrModule } from 'ngx-toastr';
import { SinhVienComponent } from './sinh-vien/sinh-vien.component';
import { UploadComponent } from './upload/upload.component';
import { LopHocEditComponent } from './lop-hoc/lop-hoc-edit/lop-hoc-edit.component';
import { MainLayoutComponent } from './shared/main-layout/main-layout.component';
import { UnauthorizeLayoutComponent } from './shared/unauthorize-layout/unauthorize-layout.component';
import { NopBaiComponent } from './nop-bai/nop-bai.component';
import { NopBaiDetailComponent } from './nop-bai-detail/nop-bai-detail.component';
import { NavMenuNopBaiComponent } from './nav-menu-nop-bai/nav-menu-nop-bai.component';
import { MonacoEditorModule, NgxMonacoEditorConfig } from 'ngx-monaco-editor';
import { LopHocChamDiemComponent } from './lop-hoc/lop-hoc-cham-diem/lop-hoc-cham-diem.component';

const monacoConfig: NgxMonacoEditorConfig = {
  baseUrl: './assets', // configure base path cotaining monaco-editor directory after build default: './assets'
  defaultOptions: { scrollBeyondLastLine: false }, // pass default options to be used
  onMonacoLoad: () => { console.log((<any>window).monaco); } // here monaco object will be available as window.monaco use this function to extend monaco editor functionalities.
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    LopHocComponent,
    LopHocAddComponent,
    LopHocEditComponent,
    SinhVienComponent,
    UploadComponent,
    MainLayoutComponent,
    UnauthorizeLayoutComponent,
    NopBaiComponent,
    NopBaiDetailComponent,
    NavMenuNopBaiComponent,
    LopHocChamDiemComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MonacoEditorModule.forRoot(monacoConfig), // use forRoot() in main app module only.
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(), // ToastrModule added
    RouterModule.forRoot([
      //Trang quản trị
      {
        path: 'admin',
        component: MainLayoutComponent,
        children: [
          { path: '', component: LopHocComponent },
          { path: 'sinh-vien', component: SinhVienComponent },
          { path: 'lop-hoc', component: LopHocComponent },
          { path: 'lop-hoc/add', component: LopHocAddComponent },
          { path: 'lop-hoc/edit/:idLopHoc', component: LopHocEditComponent },
          { path: 'lop-hoc/cham-diem/:idLopHoc', component: LopHocChamDiemComponent },
        ]
      },

      //Trang sinh viên nộp bài
      {
        path: '',
        component: UnauthorizeLayoutComponent,
        children: [
          { path: '', component: NopBaiComponent },
          { path: 'nop-bai/:idLopHoc/:maSinhVien/:hoVaTen', component: NopBaiDetailComponent },
        ]
      },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
