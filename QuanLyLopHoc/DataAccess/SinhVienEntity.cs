using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyLopHoc.DataAccess
{
    public class SinhVienEntity : BaseEntity
    {
        public SinhVienEntity() : base()
        {
                
        }

        public int IdLopHoc { get; set; }

        public string HoVaTen { get; set; }

        public string MaSinhVien { get; set; }

        public ICollection<LopHocEntity> LopHocs { get; set; }
    }
}
