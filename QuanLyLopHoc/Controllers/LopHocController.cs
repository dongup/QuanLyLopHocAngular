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
    public class LopHocController : ApiControllerBase
    {
        public LopHocController(AppDbContext context) : base(context)   
        {

        }

        // GET: api/LopHoc
        [HttpGet]
        public ResponseModel Get()
        {
            var result = _context.LopHocs
                .Select(x => new
                {
                    x.Id,   
                    x.TenLopHoc,
                    SinhViens = x.SinhViens.Select(x => new
                    {
                        x.HoVaTen
                    })
                })
                .ToList();
            return rspns.Succeed(result);
        }

        // GET api/LopHoc/5
        [HttpGet("{id}")]
        public ResponseModel Get(int id)
        {
            var result = _context.LopHocs
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.TenLopHoc,
                    SinhViens = x.SinhViens.Select(x => new
                    {
                        x.HoVaTen
                    })
                })
                .FirstOrDefault();
            return rspns.Succeed(result);
        }

        // POST api/LopHoc
        [HttpPost]
        public ResponseModel Post(LopHocRequestAdd value)
        {
            var newItem = new LopHocEntity();
            value.CopyTo(newItem);
            newItem.SinhViens = _context.SinhViens.Where(x => value.SinhVienIds.Contains(x.Id)).ToList();

            _context.LopHocs.Add(newItem);
            _context.SaveChanges();

            return rspns.Succeed();
        }

        // PUT api/LopHoc/5
        [HttpPut("{id}")]
        public ResponseModel Put(int id, LopHocRequestUpdate value)
        {
            var savedItem = _context.LopHocs
                .Where(x => x.Id == id)
                .FirstOrDefault();
            value.CopyTo(savedItem);
            savedItem.Id = id;
            _context.SaveChanges();
            return rspns.Succeed();
        }

        // DELETE api/LopHoc/5
        [HttpDelete("{id}")]
        public ResponseModel Delete(int id)
        {
            var savedItem = _context.LopHocs
                .Where(x => x.Id == id)
                .FirstOrDefault();
            savedItem.IsDeleted = true;
            _context.SaveChanges();
            return rspns.Succeed();
        }
    }
}
