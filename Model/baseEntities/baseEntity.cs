namespace WebApplication1.Model.baseEntities
{
    public class baseEntity
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
