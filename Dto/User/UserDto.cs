namespace WebApplication1.Dto.User
{
    public class UserDto
    {
        public long? Id { get; set; }
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
    }
}
