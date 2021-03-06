﻿using Microsoft.AspNet.Identity;
using ProgerBlog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.Idently
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store): base(store) {  }

        public override Task<IList<string>> GetRolesAsync(string userId)
        {
            return base.GetRolesAsync(userId);
        }
    }
}
