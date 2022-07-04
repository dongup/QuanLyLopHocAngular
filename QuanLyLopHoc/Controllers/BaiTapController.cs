using Microsoft.AspNetCore.Mvc;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.RequestModel;
using QuanLyLopHoc.Utils;
using System.Collections.Generic;
using System.Linq;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiTapController : ApiControllerBase
    {
        public BaiTapController(AppDbContext context) : base(context)   
        {

        }

        // GET: api/LopHoc
        [HttpGet("by-lop-hoc/{idLopHoc}")]
        public ResponseModel GetByMaSinhVien(int idLopHoc)
        {
            var result = _context.LopHocs
                .Where(x => x.Id == idLopHoc)
                .SelectMany(a => a.BaiTaps.Select(x => new
                {
                    x.Id, 
                    x.STT,
                    x.NoiDung,
                    x.TieuDe,
                }));
            return rspns.Succeed(result);
        }

    }
}
