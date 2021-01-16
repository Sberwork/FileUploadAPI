using System.ComponentModel.DataAnnotations;

namespace CommandBLL.Models
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Platform { get; set; }
        [Required]
        public string CommandLine { get; set; }

    }
}