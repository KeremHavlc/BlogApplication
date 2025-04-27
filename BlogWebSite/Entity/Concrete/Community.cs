namespace Entity.Concrete
{
    public class Community : BaseEntity
    {
        public byte[]? Image { get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public ICollection<CommunityPost> CommunityPosts { get; set; }
        public ICollection<CommunityUser> CommunityUsers { get; set; }

    }
}
