using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.Utils;
using System.Net.Http.Headers;

namespace QuanLyLopHoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ApiControllerBase
    {
        public UploadController(AppDbContext context, IWebHostEnvironment env) : base(context, env)
        {

        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ResponseModel> Upload()
        {
            return await saveUploadFileWithRandomName();
        }

        private async Task<ResponseModel> saveUploadFileWithRandomName()
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();

            var folderName = Path.Combine("wwwroot", "Upload", "ImportFiles");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileExtension = Path.GetExtension(fileName);
                var randomFileName = $"{Guid.NewGuid()}{fileExtension}";

                var fullPath = Path.Combine(pathToSave, randomFileName);
                var savePath = Path.Combine(folderName, randomFileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return rspns.Succeed(new FileUploadResult()
                {
                    FilePath = savePath,
                    FullServerPath = fullPath,
                    FileName = randomFileName
                });
            }
            else
            {
                return rspns.Failed();
            }
        }

    }

    public class FileUploadResult
    {
        public string FilePath { get; set; }

        public string FileName { get; set; }

        public string FullServerPath { get; set; }
    }
}
