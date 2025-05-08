namespace Core.Dtos
{
    public class CommunityPostDto
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CommunityId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
