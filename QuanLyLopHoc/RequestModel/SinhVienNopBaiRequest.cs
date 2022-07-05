using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class SinhVienNopBaiRequest { 
        public int IdLopHoc { get; set; }
        public int IdBaiTap { get; set; }
        public string MaSinhVien { get; set; }
        public string TenSinhVien { get; set; }
        public List<SinhVienTraLoiModel> TraLois { get; set; } = new List<SinhVienTraLoiModel>();
    }
}

