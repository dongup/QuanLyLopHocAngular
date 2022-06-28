using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyLopHoc.DataAccess;
using QuanLyLopHoc.Models;
using System;
using System.Data;
using System.Linq;

namespace QuanLyLopHoc.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
      
        public readonly AppDbContext _context;
       
        public readonly IWebHostEnvironment _env;

        //public int ScaleId = currentUser.ScaleId;
        public int ScaleId = 1;

        public ResponseModel rspns { get; set; } = new ResponseModel();

        public ApiControllerBase(AppDbContext context = null,
            IWebHostEnvironment webEnv = null)
        {
            _context = context;
            _env = webEnv;
        }

        public ApiControllerBase()
        {

        }
    }
}
