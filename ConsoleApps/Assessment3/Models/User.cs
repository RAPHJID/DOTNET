﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment3.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public List<Post> Posts { get; set; }
    }
}
