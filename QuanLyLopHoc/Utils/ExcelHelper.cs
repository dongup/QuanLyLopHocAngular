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

        public DataTable ReadData(string filePath)
        {
            // Open the Excel file using ClosedXML.
            // Keep in mind the Excel file cannot be open when trying to read it
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                DataTable dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;

                        foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }
                }

                return dt;
            }
        }
    }
}
