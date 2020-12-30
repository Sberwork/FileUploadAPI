using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CommandAPI.Dtos
{
    public class NewMaterialDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public IFormFile File {get; set;}
    }
}
