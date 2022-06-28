using System.ComponentModel.DataAnnotations;

namespace QuanLyLopHoc.DataAccess
{
    public class BaseEntity
    {
        public BaseEntity()
        {
                
        }

        [Key]
        public int Id { get; set; } 

        public bool IsDeleted { get; set; }
    }
}
