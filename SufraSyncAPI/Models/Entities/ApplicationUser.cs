using Microsoft.AspNetCore.Identity;

namespace SufraSyncAPI.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order> orders { get; set; }
    }
}
