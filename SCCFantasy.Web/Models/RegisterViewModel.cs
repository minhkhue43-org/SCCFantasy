﻿using System.ComponentModel.DataAnnotations;

namespace SCCFantasy.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters")]
        [RegularExpression("^[a-zA-Z0-9_-]*$", ErrorMessage = "Invalid character")]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        public string Password { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters")]
        [MinLength(8, ErrorMessage = "Minimum 8 characters")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmedPassword { get; set; }
    }
}
