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
                };
            }

            var savedCauTls = _context.SinhVienTraLois
                .Where(x => x.IdSinhVien == sinhVien.Id
                && x.IdLopHoc == value.IdLopHoc);
            _context.SinhVienTraLois.RemoveRange(savedCauTls);
            _context.SaveChanges();

            var cauTraLois = value.TraLois
                .Select(x => new SinhVienTraLoiEntity()
                {
                    IdBaiTap = x.IdBaiTap,
                    SinhVien = sinhVien,
                    IdLopHoc = value.IdLopHoc,
                    CauTraLoi = x.TraLoi,
                    CreatedDate = DateTime.Now,
                }).ToList();


            _context.SinhVienTraLois.AddRange(cauTraLois);
            _context.SaveChanges();
            return rspns.Succeed("Nộp bài thành công");
        }
       
    }
}
