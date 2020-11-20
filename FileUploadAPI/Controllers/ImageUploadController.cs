using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FileUploadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    { 
        public static IWebHostEnvironment _environment;
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUpload
        {
            public IFormFile Files
            {
                get;
                set;
            }
        }

        [HttpPost]
        public string Post([FromForm] FileUpload objfile)
        {
            if (objfile.Files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }

                    using (FileStream filestream =
                        System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + objfile.Files.FileName))
                    {
                        objfile.Files.CopyTo(filestream);
                        filestream.Flush();
                        return "\\uploads\\" + objfile.Files.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                {
                    return "Unsuccessful";
                }
            }
        }
    }
}
