namespace Entity.Concrete
{
    public class CommunityPost : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CommunityId { get; set; }
        public Community Community { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<CommunityComment> CommunityComments { get; set; }

    }
}
