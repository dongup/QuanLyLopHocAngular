using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class LopHocRequestAdd
    {
        public string TenLopHoc { get; set; }
        public List<SinhVienRequestAdd> SinhViens { get; set; } = new List<SinhVienRequestAdd>();

        public List<BaiTapRequestAdd> BaiTaps { get; set; } = new List<BaiTapRequestAdd>();
    }
}
