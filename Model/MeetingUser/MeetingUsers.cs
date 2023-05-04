using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Model.baseEntities;
using WebApplication1.Model.Meetings;
using WebApplication1.Model.User;

namespace WebApplication1.Model.MeetingUser
{
    public class MeetingUsers:baseEntity
    {
        public int MeetingId { get; set; }
        [ForeignKey("MeetingId")]
        public virtual Meeting Meeting { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ManageUser ManageUser { get; set; }

        public DateTime date { get; set; }


    }
}
