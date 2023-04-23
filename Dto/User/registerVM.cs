using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dto.User
{
    public class registerVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? emailAddress { get; set; }
        public string userName { get; set; }
        [Required]
        public string Password { get; set; }

        public string Photo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime BrithDate { get; set; }

        public string NameFatherChurch { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int? CountChildren { get; set; }
        public string? NameJop { get; set; }
        public string? IdFaceBook { get; set; }


        public string? Role { get; set; } = UserRole.user;
    }
}
