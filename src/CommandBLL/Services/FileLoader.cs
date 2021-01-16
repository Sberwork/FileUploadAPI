// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using System.Linq;
// using CommandDAL.Models;
// using Microsoft.AspNetCore.Http;
// using System;
// using System.IO;
// using Version = CommandDAL.Models.Version;
// using System.Threading.Tasks;


// namespace CommandBLL.Services
// {
//     public class FileLoader : IFileLoader
//     {
//         private const string DirName = "Files";

//         public FileLoader()
//         {
//             Directory.CreateDirectory(DirName);
//         }

//      public Version AddMaterial(Material material, IFormFile file)
//         {
//             Version newVersion;
//             if (_context.Materials.FirstOrDefault(p => p.Name == material.Name) == null)
//             {
//                 string extension = file.FileName.Split(".").Last();
//                 newVersion = new Version
//                 {
//                     Material = material,
//                     Path = _config.GetValue<string>("PathFiles" + DirName) + material.Name + "_1" + $".{extension}",
//                     Release = 1,
//                     Size = file.Length,
//                     UploadDateTime = DateTime.Now
//                 };
//                 using (var filestream = new FileStream(newVersion.Path, FileMode.Create))
//                 {
//                     file.CopyTo(filestream);
//                     filestream.Flush();
//                 }
//                 _context.Materials.Add(material);
//                 _context.Versions.Add(newVersion);
//                 _context.SaveChanges();
//                 // _backgroundJob.Enqueue(() => MailService.SendAsync($"Material with name \"{material.Name}\" was created"));
//                 return newVersion;
//             }
//             return null;
//         }

//         public Version AddVersion(string name, IFormFile file)
//         {
//             string extension = file.FileName.Split(".").Last();
//             Material material = _context.Materials.Include(p => p.Versions).FirstOrDefault(p => p.Name == name);
//             Version newVersion;
//             if (material != null)
//             {
//                 newVersion = new Version
//                 {
//                     Material = material,
//                     Path = _config.GetValue<string>("PathFiles") + material.Name + "_" + (material.Versions.Count() + 1) + $".{extension}",
//                     Release = material.Versions.Count() + 1,
//                     Size = file.Length,
//                     UploadDateTime = DateTime.Now
//                 };
//                 using (var filestream = new FileStream(newVersion.Path, FileMode.Create))
//                 {
//                     file.CopyTo(filestream);
//                     filestream.Flush();
//                 }
//                 _context.Versions.Add(newVersion);
//                 _context.SaveChanges();
//                 // _backgroundJob.Enqueue(() => MailService.SendAsync($"New version of \"{material.Name}\" was added"));
//                 return newVersion;
//             }
//             return null;
//         }



//         public byte[] DownloadFile(string name, int? version)
//         {
//             string Path;
//             byte[] mas;
//             var material = _context.Materials.Include(p => p.Versions).FirstOrDefault(p => p.Name == name);
//             if (material != null)
//             {
//                 if (version != null)
//                     Path = _config.GetValue<string>("PathFiles") + material.Name + "_" + version;
//                 else
//                     Path = _config.GetValue<string>("PathFiles") + material.Name + "_" + material.Versions.Count();
//                 mas = System.IO.File.ReadAllBytes(Path);
//                 return mas;
//             }
//             return null;
//         }



//     }
// }