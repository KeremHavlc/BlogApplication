using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class FriendShip : BaseEntity
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public bool Status { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
