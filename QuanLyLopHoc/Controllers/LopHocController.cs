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
                    SinhViens = x.SinhViens.Select(a => new
                    {
                        a.Id,
                        a.HoVaTen,
                        a.MaSinhVien
                    }),
                    BaiTaps = x.BaiTaps.Select(a => new
                    {
                        a.Id,
                        a.NoiDung,
                        a.TieuDe,
                        Diem = a.DiemSo,
                        Stt = a.STT
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
            newItem.SinhViens = value.SinhViens.Select(x => new SinhVienEntity()
            {
                HoVaTen = x.HoVaTen,
                MaSinhVien = x.MaSinhVien
            }).ToList(); 
            
            newItem.BaiTaps = value.BaiTaps.Select(x => new BaiTapEntity()
            {
                TieuDe = x.TieuDe,
                NoiDung = x.NoiDung,
                STT = x.STT
            }).ToList();

            _context.LopHocs.Add(newItem);
            _context.SaveChanges();

            return rspns.Succeed($"Đã tạo lớp học {value.TenLopHoc}!");
        }

        // PUT api/LopHoc/5
        [HttpPut("{idLopHoc}")]
        public ResponseModel Put(int idLopHoc, LopHocRequestUpdate value)
        {
            var savedItem = _context.LopHocs
                .Include(x => x.SinhViens)
                .Where(x => x.Id == idLopHoc)
                .FirstOrDefault();
            value.CopyTo(savedItem);
            savedItem.Id = idLopHoc;

            foreach(var baiTap in value.BaiTaps)
            {
                var savedBaiTap = _context.BaiTaps.Where(x => x.Id == baiTap.Id).FirstOrDefault();
                if(savedBaiTap == null)
                {
                    var newBaiTap = new BaiTapEntity()
                    {
                        Id = 0,
                        NoiDung = baiTap.NoiDung,
                        TieuDe = baiTap.TieuDe,
                        STT = baiTap.STT,
                        DiemSo = baiTap.Diem,
                        IdLopHoc = idLopHoc
                    };
                    _context.BaiTaps.Add(newBaiTap);
                }

                if(savedBaiTap != null)
                {
                    savedBaiTap.NoiDung = baiTap.NoiDung;
                    savedBaiTap.STT = baiTap.STT;
                    savedBaiTap.DiemSo = baiTap.Diem;
                }
            }

            //Tìm sinh viên đã cũ
            var savedSinhViens = _context.SinhViens
                .Where(x => value.SinhViens.Select(a => a.Id).Contains(x.Id))
                .ToList();

            //Tìm sinh viên mới
            savedSinhViens.AddRange(value.SinhViens
                      .Where(x => x.Id == 0)
                      .Select(x => new SinhVienEntity()
                      {
                          Id = 0,
                          MaSinhVien = x.MaSinhVien,
                          HoVaTen = x.HoVaTen,
                      }).ToList());
            savedItem.SinhViens = savedSinhViens;

            _context.SaveChanges();
            return rspns.Succeed($"Đã cập nhập lớp học {savedItem.TenLopHoc}!");
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
            return rspns.Succeed($"Đã xóa lớp học {savedItem.TenLopHoc}!");
        }
    }
}
