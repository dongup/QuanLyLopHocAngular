export interface LopHoc {
  tenLopHoc: string;
  id: number;
  sinhViens: SinhVien[],
  baiTaps: BaiTap[]
}

export interface BaiTap {
  id: number,
  idTraLoi?: number,
  tieuDe: string,
  noiDung: string,
  stt: number,
  traLoi?: string,
  nhanXet?: string,
  localId: string,
  coppied?: boolean,
  diem?: number,
  diemCham?: number,
}

export interface SinhVienTraLoi {
  id: number,
  maSinhVien: string,
  hoVaTen: string,
  idLopHoc: number,
  idBaiTap: number,
  traLoi: string,
  diem?: number,
  diemBaiTap?: number
}

export interface SinhVien {
  id: number,
  maSinhVien: string,
  hoVaTen: string,
  idLopHoc: number,
  nhanXet?: string,
  tongDiem?: number,
  baiTaps: BaiTap[]
}
