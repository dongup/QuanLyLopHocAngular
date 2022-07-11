using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class ChamDiemRequestModel
    {
        public int IdSinhVien { get; set; }
        public decimal TongDiem { get; set; }
        public string NhanXet { get; set; }
        public List<DiemSinhVienModel> DiemSos { get; set; }
        public decimal DiemCong { get; set; }
    }

    public class DiemSinhVienModel
    {
        public decimal Diem { get; set; }

        public int IdBaiTap { get; set; }

        public string NhanXet { get; set; }
    }
}
