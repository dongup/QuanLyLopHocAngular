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
  localId: string,
  coppied?: boolean
}

export interface SinhVienTraLoi {
  id: number,
  maSinhVien: string,
  hoVaTen: string,
  idLopHoc: number,
  idBaiTap: number,
  traLoi: string
}

export interface SinhVien {
  id: number,
  maSinhVien: string,
  hoVaTen: string,
  idLopHoc: number,
  baiTaps: BaiTap[]
}
