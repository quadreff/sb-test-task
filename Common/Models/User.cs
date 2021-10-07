using System.ComponentModel.DataAnnotations;

namespace SBTestTask.Common.Models
{
    public class User
    {
        [Required]
        [StringLength(16)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(16)]
        public string Password { get; set; } = string.Empty;
    }
}