using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyLopHoc.Utils
{
    public class ExcelService
    {
        private IWebHostEnvironment _env;
        public ExcelService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public ExcelService()
        {

        }

        public string WriteToFile(string fileName, DataTable data)
        {
            DateTime now = DateTime.Now;
            string relativePath = Path.Combine("ExelData", $"{now.Year}", $"{now.Month}", $"{now.Day}", fileName);

            string filePath = Path.Combine(_env.ContentRootPath, "wwwroot", relativePath);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(data, "Diem");
                workbook.SaveAs(filePath);
            }

            return relativePath;
        }

        public DataTable ReadData(string path)
        {
            var dataTable = new DataTable("");
            return dataTable;
        }
    }
}
