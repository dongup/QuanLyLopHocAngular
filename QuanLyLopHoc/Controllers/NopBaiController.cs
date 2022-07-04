using Microsoft.AspNetCore.Mvc;
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


        // GET: api/LopHoc
        [HttpGet("{idLopHoc}")]
        public ResponseModel GetBaiNopTheoLopHoc(int idLopHoc)
        {
            var result = _context.LopHocs
                .Where(x => x.Id == idLopHoc)
                .SelectMany(x => x.SinhViens)
                .Select(x => new
                {
                    x.HoVaTen,
                    x.MaSinhVien,
                    x.Id,
                });

            return rspns.Succeed(result);
        }


        // GET: api/LopHoc
        [HttpPost]
        public ResponseModel NopBai(List<SinhVienTraLoiRequestAdd> value)
        {
            var cauTraLois = value.Select(x => new SinhVienTraLoiEntity()
            {
                IdBaiTap = x.IdBaiTap,
                SinhVien = new SinhVienEntity()
                {
                    MaSinhVien = x.MaSinhVien,
                    HoVaTen = x.TenSinhVien,
                    IdLopHoc = x.IdLopHoc,
                },
                CauTraLoi = x.TraLoi
            }).ToList();
            _context.SinhVienTraLois.AddRange(cauTraLois);
            _context.SaveChanges();
            return rspns.Succeed("Nộp bài thành công");
        }
       
    }
}
