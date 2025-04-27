using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entity.Concrete
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostLike> Likes { get; set; }

        public ICollection<FriendShip> SentFriendships { get; set; }
        public ICollection<FriendShip> ReceivedFriendships { get; set; }
        public ICollection<CommunityPost> CommunityPosts { get; set; }
        public ICollection<CommunityComment> CommunityComments { get; set; }
        public ICollection<CommunityUser> CommunityUsers { get; set; }

    }
}
