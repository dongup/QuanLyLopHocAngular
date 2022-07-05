using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class LopHocRequestUpdate
    {
        public string TenLopHoc { get; set; }

        public List<SinhVienRequestUpdate> SinhViens { get; set; } = new List<SinhVienRequestUpdate>();

        public List<BaiTapRequestUpdate> BaiTaps { get; set; } = new List<BaiTapRequestUpdate>();
    }
}
