using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Admin.ViewModels
{
    public class UsersViewModel
    {
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 1)]
        [Required]
        public string Firstname { get; set; } = default!;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 1)]
        [Required]
        public string Lastname { get; set; } = default!;

        [EmailAddress] [Required] public string Email { get; set; } = default!;
        
        [DisplayName("New Password")]
        public string? Password { get; set; }
        
        public Guid? Id { get; set; }

        [DisplayName("User Roles")]
        public List<SelectListItem>? UserRoles { get; set; }
        public string? SelectedRole { get; set; }
    }
}