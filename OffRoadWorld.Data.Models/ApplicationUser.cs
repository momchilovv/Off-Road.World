using Microsoft.AspNetCore.Identity;

namespace OffRoadWorld.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            
        }

        public decimal Balance { get; set; } = 0;

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}