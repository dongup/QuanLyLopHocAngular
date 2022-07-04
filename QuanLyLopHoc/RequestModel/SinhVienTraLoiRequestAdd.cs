using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class SinhVienTraLoiRequestAdd { 
        public int IdLopHoc { get; set; }
        public int IdBaiTap { get; set; }
        public string MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public string TraLoi { get; set; }
    }
}

