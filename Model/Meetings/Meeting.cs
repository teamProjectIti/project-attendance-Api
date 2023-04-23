using WebApplication1.Model.Attend;
using WebApplication1.Model.baseEntities;

namespace WebApplication1.Model.Meetings
{
    public class Meeting:baseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
