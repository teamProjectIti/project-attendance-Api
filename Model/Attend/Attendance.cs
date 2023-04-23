using WebApplication1.Model.baseEntities;
using WebApplication1.Model.Meetings;
using WebApplication1.Model.User;

namespace WebApplication1.Model.Attend
{
    public class Attendance: baseEntity
    {
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public bool Attended { get; set; }=false;
        public DateTime? TimeComing  { get; set; }
    }
}
