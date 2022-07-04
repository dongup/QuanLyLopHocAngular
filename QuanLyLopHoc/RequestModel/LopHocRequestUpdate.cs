using System.Collections.Generic;

namespace QuanLyLopHoc.RequestModel
{
    public class LopHocRequestUpdate
    {
        public string TenLopHoc { get; set; }
        public List<int> SinhVienIds { get; set; } = new List<int>();

        public List<BaiTapRequestUpdate> BaiTaps { get; set; } = new List<BaiTapRequestUpdate>();
    }
}
