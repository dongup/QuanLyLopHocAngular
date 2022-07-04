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
    public class SinhVienImportController : ApiControllerBase
    {
        public SinhVienImportController(AppDbContext context, IWebHostEnvironment env) : base(context, env)   
        {

        }

        [HttpGet("from-excel-file")]
        public ResponseModel getFromExcelFile(string filePath)
        {
            string fullPath = Path.Combine(_env.ContentRootPath, filePath);
            var dataExcel = _context.FromExcel<SinhVienEntity>(fullPath);
            return rspns.Succeed(dataExcel);
        }
    }
}
