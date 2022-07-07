using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public decimal TongDiem { get; set; }

        public string NhanXet { get; set; }

        public bool DaChamDiem { get; set; }

        public DateTime ThoiGianNopBai { get; set; }

        [ForeignKey(nameof(IdLopHoc))]
        public LopHocEntity LopHoc { get; set; } 

        public ICollection<SinhVienTraLoiEntity> TraLois { get; set; } = new HashSet<SinhVienTraLoiEntity>();
    }
}
