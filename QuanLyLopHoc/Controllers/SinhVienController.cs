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
    public class SinhVienController : ApiControllerBase
    {
        public SinhVienController(AppDbContext context) : base(context)   
        {

        }

        // GET: api/SinhVien
        [HttpGet]
        public ResponseModel Get()
        {
            var result = _context.SinhViens
                .Select(x => new
                {
                    x.Id,   
                    x.HoVaTen,
                    x.MaSinhVien
                })
                .ToList();
            return rspns.Succeed(result);
        }

        // GET api/SinhVien/5
        [HttpGet("{id}")]
        public ResponseModel Get(int id)
        {
            var result = _context.SinhViens
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.HoVaTen,
                    x.MaSinhVien
                })
                .FirstOrDefault();
            return rspns.Succeed(result);
        }

        // POST api/SinhVien
        [HttpPost]
        public ResponseModel Post(SinhVienRequestAdd value)
        {
            var newItem = new SinhVienEntity();
            value.CopyTo(newItem);

            _context.SinhViens.Add(newItem);
            _context.SaveChanges();

            return rspns.Succeed($"Đã tạo sinh viên {value.HoVaTen}!");
        }

        // PUT api/SinhVien/5
        [HttpPut("{id}")]
        public ResponseModel Put(int id, SinhVienRequestUpdate value)
        {
            var savedItem = _context.SinhViens
                .Where(x => x.Id == id)
                .FirstOrDefault();
            value.CopyTo(savedItem);
            savedItem.Id = id;
            _context.SaveChanges();
            return rspns.Succeed($"Đã cập nhập sinh viên {id}!");
        }

        // DELETE api/SinhVien/5
        [HttpDelete("{id}")]
        public ResponseModel Delete(int id)
        {
            var savedItem = _context.SinhViens
                .Where(x => x.Id == id)
                .FirstOrDefault();
            if (savedItem == null) return rspns.Failed($"Sinh viên có id {id} không tồn tại!");

            savedItem.IsDeleted = true;
            _context.SaveChanges();
            return rspns.Succeed($"Đã xóa sinh viên {savedItem.HoVaTen}!");
        }
    }
}
