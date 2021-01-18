using System;
using System.Collections.Generic;
using Moq;
using AutoMapper;
using CommandBLL.Models;
using Xunit;
using CommandAPI.Controllers;
using CommandDAL.Data;
using CommandBLL.Profiles;
using Microsoft.AspNetCore.Mvc;
using CommandDAL.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CommandBLL.Services;
using Npgsql;
using Microsoft.Extensions.DependencyInjection;

namespace CommandAPI.Tests
{
    public class UnitTests
    {
        // public IConfiguration Configuration { get; }
        // public UnitTests(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }
        // public void ConfigureServices(IServiceCollection services)
        // {
        //     var builder = new NpgsqlConnectionStringBuilder();
        //     builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
        //     builder.Username = Configuration["UserID"];
        //     builder.Password = Configuration["Password"];

        //     services.AddDbContext<ApplicationContext>(opt => opt.UseNpgsql(builder.ConnectionString));

        // }

        private MaterialService _materialService = new MaterialService(
new ApplicationContext(new DbContextOptionsBuilder<ApplicationContext>()
    .UseNpgsql("Host=localhost;Port=5432;Database=CmdAPI;Pooling=true;")
    .Options), new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build());

        [Fact]
        public void GetMaterialById_GetMaterialWithId1_NotNull()
        {

            var result = _materialService.GetMaterialById(1);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetMaterialByName_GetMaterialWithNameTestFile_NotNull()
        {
            var result = _materialService.GetMaterialByName("TestFile");

            Assert.NotNull(result);
        }

        [Fact]

        public void ChangeMaterialCategory_GetMaterialWithEditCategory_Other()
        {
            Material material = _materialService.GetMaterialById(1);

            var result = _materialService.ChangeMaterialCategory(material.Name, "Other");

            Assert.Equal("Other", result.Category);
        }

        [Fact]

        public void AddMaterial_AddEmptyFile_NotNull()
        {
            Material material = new Material { Name = "newmaterial", Category = "Other" };
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("new")), 0, 0, "Data", "text1.txt");

            var result = _materialService.AddMaterial(material, file);

            Assert.NotNull(result);
        }
    }
}