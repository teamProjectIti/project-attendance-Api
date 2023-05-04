using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model.baseEntities
{
    public class baseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
