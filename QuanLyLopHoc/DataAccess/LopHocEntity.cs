using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyLopHoc.DataAccess
{
    public class LopHocEntity : BaseEntity
    {
        public LopHocEntity() : base()
        {
                
        }

        public string TenLopHoc { get; set; }

        public ICollection<SinhVienEntity> SinhViens { get; set; }
        public ICollection<BaiTapEntity> BaiTaps { get; set; }
    }
}
