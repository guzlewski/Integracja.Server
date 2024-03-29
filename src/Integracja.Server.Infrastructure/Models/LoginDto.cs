﻿using System.ComponentModel.DataAnnotations;

namespace Integracja.Server.Infrastructure.Models
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
