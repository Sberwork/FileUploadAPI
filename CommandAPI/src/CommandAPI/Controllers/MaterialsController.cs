using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CommandAPI;
using CommandAPI.Dtos;
using CommandAPI.Infrastructure;
using CommandDAL.Data;
using CommandDAL.Models;
using Microsoft.Extensions.Configuration;
using CommandBLL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        private readonly long _fileSizeLimit;
        private List<string> Categories = new List<string> { "Presentation", "Application", "Other" };
        public MaterialsController(IMaterialService materialService, IConfiguration config)
        {
            _materialService = materialService;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");
        }

        [HttpGet]
        public ActionResult<IList<Material>> GetAllMaterials(string category)
        {
            if (category == null)
                return Ok(_materialService.GetAllMaterials());
            if (Categories.Contains(category))
            {
                var materials = _materialService.GetFilteredMaterials(category);
                if (materials != null)
                    return materials.ToList();
            }
            return BadRequest();
        }

        // GET: api/Material/{id}
        [HttpGet("{id}")]
        public ActionResult<Material> GetMaterialsById(int id)
        {
            var material = _materialService.GetMaterialById(id);
            if (material != null)
                return Ok(material);
            return BadRequest();
        }

        // GET: api/Material/{name}
        [HttpGet]
        [Route("name")]
        public IActionResult GetMaterialByName(string name)
        {
            var material = _materialService.GetMaterialByName(name);
            if (material != null)
                return Ok(material);
            return BadRequest();
        }

        [HttpGet]
        [Route("download")]
        public IActionResult DownloadFile(string name, int? version)
        {
            var result = _materialService.DownloadFile(name, version);
            if (result != null)
            {
                return File(result, "application/octet-stream", name);
            }
            return BadRequest();
        }

        [HttpPatch]
        [Authorize(Roles = "initiator, admin")]
        public ActionResult<Material> ChangeMaterialCategory(string name, string category)
        {
            if (Categories.Contains(category))
            {
                var material = _materialService.ChangeMaterialCategory(name, category);
                if (material != null)
                {
                    return Ok(material);
                }
            }
            return BadRequest();
        }

        // POST: api/Material
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]
        [RequestSizeLimit(2147483648)]
        [Authorize(Roles = "initiator, admin")]
        public IActionResult AddMaterial([FromForm] NewMaterialDto material)
        {
            if (material.Name != null && material.Category != null && material.File != null
                && material.File.Length < _fileSizeLimit && Categories.Contains(material.Category))
            {
                Material newMaterial = new Material { Name = material.Name, Category = material.Category };
                var result = _materialService.AddMaterial(newMaterial, material.File);
                if (result != null)
                    return Ok();
            }
            return BadRequest();
        }

        // POST: api/Material/add
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "initiator, admin")]
        public IActionResult AddVersion([FromForm] UpdateMaterialDto material)
        {
            if (material.Name != null && material.File != null)
            {
                var result = _materialService.AddVersion(material.Name, material.File);
                if (result != null)
                    return Ok();
            }
            return BadRequest();
        }
    }
}
