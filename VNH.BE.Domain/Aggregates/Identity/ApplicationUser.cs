using Microsoft.AspNetCore.Identity;

namespace VNH.BE.Domain.Aggregates.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public int AvatarId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
