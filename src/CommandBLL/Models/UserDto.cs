using System.ComponentModel.DataAnnotations;

namespace CommandBLL.Models
{
    public class UserDto
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}