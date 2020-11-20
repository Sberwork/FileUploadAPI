﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.IO;
using WebApplication12.Models;

namespace WebApplication12.Controllers
{
    public class DemoController : Controller
    {
        private readonly DatabaseContext _context;
        public DemoController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile files)
        {
            if (files != null)
            {
                if (files.Length > 0)
                {
                    //Getting FileName
                    var fileName = Path.GetFileName(files.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = String.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var objfiles = new Files()
                    {
                        DocumentId = 0,
                        Name= newFileName,
                        FileType = fileExtension,
                        CreatedOn = DateTime.Now
                    };
                    
                    using (var target = new MemoryStream())
                    {
                        files.CopyTo(target);
                        objfiles.DataFiles = target.ToArray();
                    }

                    _context.Files.Add(objfiles);
                    _context.SaveChanges();

                }
            }
            return View();
        }

        

    }
}