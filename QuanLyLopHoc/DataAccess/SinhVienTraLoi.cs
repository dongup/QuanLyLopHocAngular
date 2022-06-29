using System.ComponentModel.DataAnnotations;

namespace QuanLyLopHoc.DataAccess
{
    public class SinhVienTraLoiEntity : BaseEntity
    {
        public SinhVienTraLoiEntity() : base()
        {
                
        }

        public int IdSinhVien { get; set; }

        public int IdBaiTap { get; set; }

        public string CauTraLoi { get; set; }
    }
}
