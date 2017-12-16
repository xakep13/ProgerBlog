using Microsoft.AspNet.Identity.EntityFramework;
using ProgerBlog.DAL.Context;
using ProgerBlog.DAL.Entities;
using ProgerBlog.DAL.Idently;
using ProgerBlog.DAL.Interfaces;
using ProgerBlog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private IPostManager postManager;

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
            postManager = new PostManager(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IPostManager PostManager
        {
            get { return postManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager GetRoleManager()
        { return roleManager; }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                    postManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
