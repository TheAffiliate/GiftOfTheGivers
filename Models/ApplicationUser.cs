﻿using Microsoft.AspNetCore.Identity;

namespace GiftOfTheGivers_ST10239864.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
