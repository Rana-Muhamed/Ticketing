using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models
{
    public class ApplicationUser:IdentityUser
    {
        [ MaxLength(128)]
        public string FirstName { get; set; }
        [ MaxLength(128)]
        public string LastName { get; set; }
    }
}
