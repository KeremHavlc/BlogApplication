namespace Core.Dtos
{
    public class PostDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid UserId { get; set; }
    }
}
