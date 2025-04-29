namespace Core.Dtos
{
    public class CommunityUsersCheckDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Guid? UserId { get; set; }
    }
}
