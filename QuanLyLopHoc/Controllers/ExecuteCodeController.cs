using Microsoft.AspNetCore.Mvc;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.RequestModel;
using QuanLyLopHoc.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SFile = System.IO.File;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/execute-code")]
    [ApiController]
    public class ExecuteCodeController : ApiControllerBase
    {
        public ExecuteCodeController(AppDbContext context, IWebHostEnvironment env) : base(context, env)   
        {

        }

        // POST: api/LopHoc
        [HttpPost]
        public async Task<ResponseModel> ExecuteCode(SinhVienNopBaiRequest traLoi)
        {
            string tenLopHoc = _context.LopHocs.Where(x => x.Id == traLoi.IdLopHoc).Select(x => x.TenLopHoc).FirstOrDefault();
            int sttCauHoi = _context.BaiTaps.Where(x => x.Id == traLoi.IdBaiTap).Select(x => x.STT).FirstOrDefault();
            

            string folderPath = Path.Combine(_env.ContentRootPath
                , "wwwroot"
                , "Code"
                , $"{tenLopHoc.RemoveUnicode().Replace(" ", "_")}"
                , $"{traLoi.MaSinhVien}-{traLoi.TenSinhVien.RemoveUnicode().Replace(" ", "_").Replace('/', '_').Replace('\\', '_')}");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, $"cau_{sttCauHoi}.c");
            //await WriteToFile(filePath, traLoi.TraLoi);

            string execFilePath = Path.Combine(folderPath, $"cau_{sttCauHoi}.exe");

            string strCmdText;
            strCmdText = $"gcc {filePath} -o {execFilePath}";
            var execComplite = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            //execComplite.WaitForExit();
            execComplite.Kill();
            Console.WriteLine(strCmdText);

            ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", strCmdText);
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = false;
            procStartInfo.WindowStyle = ProcessWindowStyle.Normal;
            procStartInfo.ErrorDialog = true;

            Console.WriteLine("starting...");

            Process process = new Process();
            process.StartInfo = procStartInfo;
            process.Start();
            //process.Kill();

            string result = process.StandardOutput.ReadToEnd();
            Console.WriteLine("started");
            Console.WriteLine("result: " + result);

            string error = process.StandardError.ReadToEnd();
            Console.WriteLine("error: " + error);

            //string strCmdTextExec;
            //strCmdTextExec = $"{execFilePath}";
            //var process = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            //process.WaitForExit();
            //process.BeginOutputReadLine();
            //process.StandardOutput();

            return rspns.Succeed();
        }

        private async Task WriteToFile(string filePath, string text)
        {
            await SFile.WriteAllTextAsync(filePath, text);
        }
    }
}

