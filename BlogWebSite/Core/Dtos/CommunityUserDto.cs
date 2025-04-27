namespace Core.Dtos
{
    public class CommunityUserDto 
    {
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
