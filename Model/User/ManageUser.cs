using WebApplication1.Model.Attend;
using WebApplication1.Model.baseEntities;

namespace WebApplication1.Model.User
{
    public class ManageUser:baseEntity
    {
        public string UserName { get; set; } = string.Empty;
        public int Mobile { get; set; } 
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime BrithDate { get; set; }
        public string? NameFatherChurch { get; set; } = string.Empty;
        public bool Status { get; set; }
        public int? CountChildren { get; set; }
        public string? NameJop { get; set; }
        public string? IdFaceBook { get; set; }
        public ICollection<Attendance> Attendances { get; set; }

    }
}
