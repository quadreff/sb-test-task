using System.ComponentModel.DataAnnotations;

namespace SBTestTask.WebApi.Models
{
    public class AuthInfo
    {
        [Required]
        [StringLength(16)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16)]
        public string Password { get; set; }
    }
}