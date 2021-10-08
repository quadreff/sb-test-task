﻿using System.ComponentModel.DataAnnotations;

namespace SBTestTask.WebApi.Models
{
    public class AuthInfo
    {
        [Required]
        [StringLength(16)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(16)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}