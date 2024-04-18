using DotNetAuthentication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetAuthentication.Models
{
    public class Profiles
    {
        [Key]
        public int ProfileId { get; set; }

        public string Platform { get; set; }

        public string Link { get; set; }

        public string? UserId { get; set; }
        public Users? User { get; set; } 
    }
}
