﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgerBlog.WEB.Models
{
    public class EditModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public bool IsDelete { get; set; }
    }
}
