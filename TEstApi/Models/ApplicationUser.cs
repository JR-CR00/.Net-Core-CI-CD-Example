using System;
using Microsoft.AspNetCore.Identity;

namespace TEstApi.Models;

public class ApplicationUser : IdentityUser
{
    public string? Name { get; set; }
}
