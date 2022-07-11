using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.RequestModel;
using QuanLyLopHoc.Utils;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/nop-bai")]
    [ApiController]
    public class NopBaiController : ApiControllerBase
    {
        public NopBaiController(AppDbContext context) : base(context)   
        {

        }

        // POST: api/LopHoc
        [HttpPost]
        public ResponseModel NopBai(SinhVienNopBaiRequest value)
        {
            var lopHoc = _context.LopHocs.Where(x => x.Id == value.IdLopHoc).FirstOrDefault();
            var sinhVien = _context.SinhViens
                                    .Where(x => x.IdLopHoc == value.IdLopHoc
                                            && x.MaSinhVien == value.MaSinhVien
                                            && x.HoVaTen == value.TenSinhVien).FirstOrDefault();
            if(sinhVien == null)
            {
                sinhVien = new SinhVienEntity()
                {
                    MaSinhVien = value.MaSinhVien,
                    HoVaTen = value.TenSinhVien,
                    IdLopHoc = value.IdLopHoc,
                    ThoiGianNopBai = DateTime.Now,
                };
            }

            foreach(var traLoiRequest in value.TraLois)
            {
                var savedItem = _context.SinhVienTraLois
                        .Where(x => x.IdBaiTap == traLoiRequest.IdBaiTap
                                 && x.IdSinhVien == sinhVien.Id)
                            .FirstOrDefault();
                if(savedItem == null)
                {
                    savedItem = new SinhVienTraLoiEntity()
                    {
                        IdBaiTap = traLoiRequest.IdBaiTap,
                        SinhVien = sinhVien,
                        IdLopHoc = value.IdLopHoc,
                        CauTraLoi = traLoiRequest.TraLoi,
                        CreatedDate = DateTime.Now,
                    };
                    _context.SinhVienTraLois.Add(savedItem);
                }

                if(savedItem != null)
                {
                    savedItem.CauTraLoi = traLoiRequest.TraLoi;
                }
            }
           
            _context.SaveChanges();

            return rspns.Succeed("Nộp bài thành công");
        }
       
    }
}
