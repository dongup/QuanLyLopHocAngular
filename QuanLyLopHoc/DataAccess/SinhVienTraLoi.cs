using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyLopHoc.DataAccess
{
    public class SinhVienTraLoiEntity : BaseEntity
    {
        public SinhVienTraLoiEntity() : base()
        {
                
        }

        public int IdSinhVien { get; set; }

        public int IdBaiTap { get; set; }

        public int IdLopHoc { get; set; }

        public string CauTraLoi { get; set; }

        public int Diem { get; set; }

        public string NhanXet { get; set; }

        [ForeignKey(nameof(IdSinhVien))]
        public virtual SinhVienEntity SinhVien { get; set; }

        [ForeignKey(nameof(IdBaiTap))]
        public virtual BaiTapEntity BaiTap { get; set; }
    }
}
