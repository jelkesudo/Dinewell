﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinewell.Domain.Entities
{
    public class UserAddress : Entity
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
