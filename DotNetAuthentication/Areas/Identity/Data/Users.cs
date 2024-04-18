using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DotNetAuthentication.Models;
using Microsoft.AspNetCore.Identity;

namespace DotNetAuthentication.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Users class
public class Users : IdentityUser
{
    [PersonalData]
    [Required]
    public string FirstName { get; set; }

    [PersonalData]
    [Required]  
    public string LastName { get; set; }

    public List<Profiles>? Profiles { get; set; }
}

