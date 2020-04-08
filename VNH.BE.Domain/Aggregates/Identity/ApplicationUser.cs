using Microsoft.AspNetCore.Identity;

namespace VNH.BE.Domain.Aggregates.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public int AvatarId { get; set; }
    }
}
