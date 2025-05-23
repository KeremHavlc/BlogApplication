﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }

        public User User { get; set; }
        public Post Post { get; set; }
    }
}
