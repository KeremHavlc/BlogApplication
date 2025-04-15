using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
