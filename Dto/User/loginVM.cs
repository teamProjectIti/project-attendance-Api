using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.User
{
    public class loginVM
    {
        [Required]
        public string emailAddress { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } =  string.Empty;
    }
}
