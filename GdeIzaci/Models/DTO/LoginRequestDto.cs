using System.ComponentModel.DataAnnotations;

namespace GdeIzaci.Models.DTO
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        [MaxLength(10, ErrorMessage = "Username has to be maximum of 10 characters")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password has to be minimum of 6 characters")]
        public string Password { get; set; }
    }
}
