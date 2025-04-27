namespace Core.Dtos
{
    public class CommunityCommentDto
    {
        public string Comment { get; set; }
        public Guid CommunityPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
