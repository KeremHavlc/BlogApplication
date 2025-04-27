namespace Entity.Concrete
{
    public class CommunityUser : BaseEntity
    {
        public Guid CommunityId { get; set; }
        public Community Community { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
