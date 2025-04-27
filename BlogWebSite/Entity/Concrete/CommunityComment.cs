namespace Entity.Concrete
{
    public class CommunityComment : BaseEntity
    {
        public Guid CommunityPostId { get; set; }
        public CommunityPost CommunityPost { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
