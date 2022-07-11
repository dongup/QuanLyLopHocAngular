using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.RequestModel;
using QuanLyLopHoc.Utils;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ApiControllerBase
    {
        public ImportController(AppDbContext context, IWebHostEnvironment env) : base(context, env)   
        {

        }

        [HttpPost("sinh-vien")]
        public ResponseModel getFromExcelFile(FilePathModel filePath)
        {
            string fullPath = Path.Combine(_env.ContentRootPath, filePath.FilePath);
            var dataExcel = _context.FromExcel<SinhVienEntity>(fullPath);
            return rspns.Succeed(dataExcel);
        }


        /// <summary>
        /// Import bai lam
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpPost("bai-lam/{idLopHoc}")]
        public ResponseModel importBaiLam(int idLopHoc, FilePathModel filePath)
        {
            string fullPath = Path.Combine(_env.ContentRootPath, filePath.FilePath);
            var dataExcel = _context.GetDataTableFromExcel(fullPath);
            //Console.WriteLine(dataExcel.Skip(3).FirstOrDefault().HoTenSv);
            var baiTaps = _context.LopHocs.Include(x => x.BaiTaps).Where(x => x.Id == idLopHoc).FirstOrDefault().BaiTaps.ToList();

            foreach (DataRow dataRow in dataExcel.Rows)
            {
                // DauThoiGian,TongDiem,MaSinhVien,HoTenSv,Cau01,Cau02,Cau03,Cau04,Cau05
                string maSv = Convert.ToString(dataRow["MÃ SINH VIÊN"]);
                string hoTen = Convert.ToString(dataRow["HỌ TÊN SV"]);
                DateTime ngayNopBai = dataRow["Dấu thời gian"].ToString().ToDateTimeGTM();
                Console.WriteLine("importing sv: " + maSv);

                var sinhVien = _context.SinhViens
                                    .Where(x => x.IdLopHoc == idLopHoc
                                            && x.MaSinhVien == maSv
                                            && x.HoVaTen == hoTen)
                                    .FirstOrDefault();
                if(sinhVien == null)
                {
                    sinhVien = new SinhVienEntity()
                    {
                        MaSinhVien = maSv,
                        HoVaTen = hoTen,
                        IdLopHoc = idLopHoc,
                        CreatedDate = DateTime.Now,
                        ThoiGianNopBai = ngayNopBai,
                    };
                    _context.Add(sinhVien);
                    _context.SaveChanges();
                }

                if (sinhVien != null)
                {
                    sinhVien.ThoiGianNopBai = ngayNopBai;
                }


                int stt = 1;
                for(int i = 3; i < dataRow.ItemArray.Length; i++)
                {
                    var savedItem = _context.SinhVienTraLois
                       .Where(x => x.BaiTap.STT == stt
                                && x.IdSinhVien == sinhVien.Id)
                           .FirstOrDefault();
                    if (savedItem == null)
                    {
                        savedItem = new SinhVienTraLoiEntity()
                        {
                            IdBaiTap = baiTaps.Where(x => x.STT == stt).Select(x => x.Id).FirstOrDefault(),
                            IdSinhVien = sinhVien.Id,
                            IdLopHoc = idLopHoc,
                            CauTraLoi = dataRow[i].ToString(),
                            CreatedDate = ngayNopBai,
                            Diem = 0,
                            NhanXet = "",
                        };
                        _context.SinhVienTraLois.Add(savedItem);
                    }

                    if (savedItem != null)
                    {
                        savedItem.CauTraLoi = dataRow[i].ToString();
                        savedItem.CreatedDate = ngayNopBai;
                    }

                    stt++;
                }
            }

            _context.SaveChanges();
            return rspns.Succeed("Import bài làm thành công!");
        }

        public class FilePathModel
        {
            public string FilePath { get; set; }
        }
    }
}
