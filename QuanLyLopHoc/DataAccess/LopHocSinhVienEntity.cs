using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyLopHoc.DataAccess
{
    public class LopHocSinhVienEntity : BaseEntity
    {
        public LopHocSinhVienEntity() : base()
        {
                
        }

        public int LopHocsId { get; set; }
        public LopHocEntity LopHoc { get; set; }

        public int SinhViensId { get; set; }
        public SinhVienEntity SinhVien { get; set; }

    }
}
