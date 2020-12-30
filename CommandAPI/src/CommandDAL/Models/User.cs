using Microsoft.AspNetCore.Identity;

namespace CommandDAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName {get; set;}
    }
}