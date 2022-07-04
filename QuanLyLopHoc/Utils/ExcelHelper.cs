using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyLopHoc.Utils
{
    public class ExcelHelper
    {
        public static XSSFWorkbook hssfworkbook = new XSSFWorkbook();

        //private string _rootPath = "";

        public ExcelHelper()
        {

        }

        public ExcelHelper(string templatePath)
        {
            InitializeWorkbook(templatePath);
        }

        private static void InitializeWorkbook(string path)
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            hssfworkbook = new XSSFWorkbook(file);
        }

        public void WriteToFile(string path)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path, FileMode.Create);
            hssfworkbook.Write(file);
            file.Close();
        }

        public static DataTable ReadData(string path)
        {
            Console.WriteLine("Reading data");
            InitializeWorkbook(path);

            var sheet = hssfworkbook.GetSheetAt(0); // zero-based index of your target sheet
            var dataTable = new DataTable(sheet.SheetName);

            // write the header row
            var headerRow = sheet.GetRow(0);
            foreach (var headerCell in headerRow)
            {
                dataTable.Columns.Add(headerCell?.ToString());
            }

            Console.WriteLine("Filled header");

            // write the rest
            for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)
            {
                Console.WriteLine("Writing");
                var sheetRow = sheet.GetRow(i);
                var dtRow = dataTable.NewRow();
                dtRow.ItemArray = dataTable.Columns
                    .Cast<DataColumn>()
                    .Select(c => sheetRow?.GetCell(c.Ordinal, MissingCellPolicy.CREATE_NULL_AS_BLANK)?.ToString())
                    .ToArray();
                dataTable.Rows.Add(dtRow);
            }

            return dataTable;
        }
    }
}
