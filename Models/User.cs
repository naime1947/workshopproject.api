using Microsoft.AspNetCore.Identity;

namespace workshopproject.API.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
    }
}