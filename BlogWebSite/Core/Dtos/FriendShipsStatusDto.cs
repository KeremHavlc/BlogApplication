using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class FriendShipsStatusDto
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Guid? UserId { get; set; }
        public string? Username{ get; set; }
        public string? Email { get; set; }     
    }
}
