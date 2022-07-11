using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyLopHoc.DataAccess
{
    public class BaiTapEntity : BaseEntity
    {
        public BaiTapEntity()
        {
                
        }

        public int IdLopHoc { get; set; }

        public int STT { get; set; }

        public string TieuDe { get; set; }

        public string NoiDung { get; set; }

        public decimal DiemSo { get; set; }

        [ForeignKey(nameof(IdLopHoc))]
        public virtual LopHocEntity LopHoc { get; set; }
    }
}
