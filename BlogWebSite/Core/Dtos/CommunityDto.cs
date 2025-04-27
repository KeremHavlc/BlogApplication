namespace Core.Dtos
{
    public class CommunityDto
    {
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
