using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using CommandDAL.Data;
using CommandDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Version = CommandDAL.Models.Version;
using System.Threading.Tasks;
using Hangfire;


namespace CommandBLL.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _config;
        public MaterialService(ApplicationContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        public IList<Material> GetAllMaterials()
        {
            return _context.Materials.Include(p => p.Versions).ToList<Material>();
        }
        public Material GetMaterialByName(string name)
        {
            var material = _context.Materials.Include(p => p.Versions).Where(p => p.Name == name).FirstOrDefault();
            if (material == null)
                return null;
            return material;
        }

        public Material GetMaterialById(int id)
        {
            var material = _context.Materials.Include(p => p.Versions).FirstOrDefault(p => p.Id == id);
            if (material == null)
                return null;
            return (Material)material;
        }

        public Version AddMaterial(Material material, IFormFile file)
        {
            
            FileLoader fileLoader = new FileLoader(_context, _config);
            return fileLoader.AddFile(material, file);
        }

        public Version AddVersion(string name, IFormFile file)
        {
            Material material = _context.Materials.Include(p => p.Versions).FirstOrDefault(p => p.Name == name);
            FileLoader fileLoader = new FileLoader(_context, _config);
            return fileLoader.AddFile(material, file);
        }
        public IList<Material> GetFilteredMaterials(string category)
        {
            var materials = from m in _context.Materials.Include(p => p.Versions)
                            select m;
            var filteredMaterials = materials.Where(s => s.Category == category);
            if (filteredMaterials != null)
                return filteredMaterials.ToList();
            return null;
        }

        public Material ChangeMaterialCategory(string name, string category)
        {
            var material = _context.Materials.Include(p => p.Versions).Where(p => p.Name == name).FirstOrDefault();
            if (material != null)
            {
                material.Category = category;
                _context.SaveChanges();
                return material;
            }
            return null;
        }

        public byte[] DownloadFile(string name, int? version)
        {

            FileLoader fileLoader = new FileLoader(_context, _config);
            return fileLoader.DownloadFile(name, version);

        }

    }
}

// сервисы, которые у тебя в бизнес-слое, не попадают под него. сервисы в бизнес-логике должны только оперировать с бизнес-моделями. я бы эти сервисы в основной проект вынес. и у тебя как минимум не хватает самих бизнес-моделей