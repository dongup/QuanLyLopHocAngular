﻿using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class ChamDiemRequestModel
    {
        public int IdSinhVien { get; set; }
        public int TongDiem { get; set; }
        public string NhanXet { get; set; }
        public List<DiemSinhVienModel> DiemSos { get; set; }
    }

    public class DiemSinhVienModel
    {
        public int Diem { get; set; }

        public int IdBaiTap { get; set; }

        public string NhanXet { get; set; }
    }
}
