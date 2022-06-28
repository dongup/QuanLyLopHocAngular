using System.ComponentModel.DataAnnotations;

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
    }
}
