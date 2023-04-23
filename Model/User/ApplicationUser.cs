using Microsoft.AspNetCore.Identity;
using WebApplication1.Model.Attend;

namespace WebApplication1.Model.User
{
    public class ApplicationUser : IdentityUser
    {
        public string Photo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime BrithDate { get; set; }

        public string NameFatherChurch { get; set; } = string.Empty;

        public bool Status { get; set; }
        public int? CountChildren { get; set; }
        public string? NameJop { get; set; }
        public string? IdFaceBook { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
