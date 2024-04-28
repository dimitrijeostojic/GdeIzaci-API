﻿using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
