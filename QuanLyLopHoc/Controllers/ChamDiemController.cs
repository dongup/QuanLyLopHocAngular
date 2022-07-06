﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.RequestModel;
using QuanLyLopHoc.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/cham-diem")]
    [ApiController]
    public class ChamDiemController : ApiControllerBase
    {
        private readonly ExcelService _excel;

        public ChamDiemController(AppDbContext context, ExcelService excel) : base(context)   
        {
            _excel = excel;
        }

        // GET: api/LopHoc
        [HttpGet("excel/{idLopHoc}")]
        public ResponseModel GetFileExcelDiem(int idLopHoc)
        {
            var lopHoc = _context.LopHocs.Where(x => x.Id == idLopHoc).FirstOrDefault();

            string query = @"
                SELECT Id AS [Id sinh viên]
                    , MaSinhVien AS [Mã sinh viên]
                    , HoVaTen AS [Họ và tên]
                    , TongDiem AS [Tổng điểm]
                FROM SinhViens 
                WHERE IdLopHoc = {0}
            ";
            //Console.WriteLine(query);

            var dataExcel = _context.GetDateTable(query, idLopHoc);

            string fileName = $"Kết quả lớp {lopHoc.TenLopHoc.RemoveForFileName()}.xlsx";
            string filePath = _excel.WriteToFile(fileName, dataExcel);

            return rspns.Succeed(filePath);
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
                    x.TongDiem,
                    x.NhanXet,
                    BaiTaps = x.TraLois.Select(a => new
                    {
                        IdTraLoi = a.Id,
                        Id = a.IdBaiTap,
                        a.BaiTap.NoiDung,
                        a.BaiTap.STT,
                        TraLoi = a.CauTraLoi,
                        a.BaiTap.TieuDe,
                        Diem = a.BaiTap.DiemSo,
                        DiemCham = a.Diem,
                        a.NhanXet,
                    })
                });

            return rspns.Succeed(result);
        }

        // POST: api/cham-diem
        [HttpPost]
        public ResponseModel ChamDiem(ChamDiemRequestModel value)
        {
            var sinhVien = _context.SinhViens.Where(x => x.Id == value.IdSinhVien).FirstOrDefault();
            if (sinhVien == null) return rspns.Failed("Id sinh viên không tồn tại " + value.IdSinhVien);

            sinhVien.TongDiem = value.TongDiem;
            sinhVien.NhanXet = value.NhanXet;

            foreach(var diem in value.DiemSos)
            {
                var baiLamSinhVien = _context.SinhVienTraLois
                    .Where(x => x.IdSinhVien == value.IdSinhVien
                                && x.IdBaiTap == diem.IdBaiTap)
                    .FirstOrDefault();
                if(baiLamSinhVien != null)
                {
                    baiLamSinhVien.Diem = diem.Diem;
                    baiLamSinhVien.NhanXet = diem.NhanXet;
                }
            }

            _context.SaveChanges();
            return rspns.Succeed($"Chấm điểm sinh viên {sinhVien.MaSinhVien} thành công");
        }
       
    }
}
