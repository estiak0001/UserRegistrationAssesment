using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace UserRegistrationAssesment.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
