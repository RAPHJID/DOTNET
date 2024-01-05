﻿using System.ComponentModel.DataAnnotations;

namespace E_Authentication.Models.Dtos
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password {  get; set; } = string.Empty;
       
    }
}
