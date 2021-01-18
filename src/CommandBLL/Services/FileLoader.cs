using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using CommandDAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Version = CommandDAL.Models.Version;
using System.Threading.Tasks;
using CommandDAL.Data;

namespace CommandBLL.Services
{
    public class FileLoader
    {
        private readonly IConfiguration _config;

        private readonly ApplicationContext _context;
        public FileLoader(ApplicationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Version AddFile(Material material, IFormFile file)
        {
            string extension = file.FileName.Split(".").Last();
            int releaseNumber;
            var path = _config.GetValue<string>("PathFiles");
            if (material != null)
            {
                if (_context.Materials.FirstOrDefault(p => p.Name == material.Name) == null)
                {
                    releaseNumber = 1;
                    _context.Materials.Add(material);
                }
                else
                {
                    releaseNumber = material.Versions.Count() + 1;
                }

                Version newVersion = new Version
                {
                    Material = material,
                    Release = releaseNumber,
                    Path = path + material.Name + "_№" + releaseNumber.ToString() + $".{extension}",
                    Size = file.Length,
                    UploadDateTime = DateTime.Now
                };
                using (var filestream = new FileStream(newVersion.Path, FileMode.Create))
                {
                    file.CopyTo(filestream);
                    filestream.Flush();
                }
                _context.Versions.Add(newVersion);
                _context.SaveChanges();
                return newVersion;
            }
            return null;
        }

        public byte[] DownloadFile(string name, int? version)
        {

            var path = _config.GetValue<string>("PathFiles");
            string Path;
            byte[] mass;

            var material = _context.Materials.Include(p => p.Versions).FirstOrDefault(p => p.Name == name);
            if (material != null)
            {
                if (version != null)
                    Path = path + material.Name + "_" + version;
                else
                    Path = path + material.Name + "_" + material.Versions.Count();
                mass = System.IO.File.ReadAllBytes(Path);
                return mass;
            }
            return null;
        }
    }
}