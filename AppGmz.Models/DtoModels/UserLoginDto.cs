using System.ComponentModel.DataAnnotations;

namespace AppGmz.Models.DtoModels
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}