using ProgerBlog.DAL.Context;
using ProgerBlog.DAL.Entities;
using ProgerBlog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public ApplicationContext Database { get; set; }
        public ClientManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Delete(ClientProfile item)
        {
           ClientProfile clientProfile= Database.ClientProfiles.Find(item);
            if (clientProfile != null)
                Database.ClientProfiles.Remove(clientProfile);
        }

        public void Delete(int id_item)
        {
            ClientProfile clientProfile = Database.ClientProfiles.Find(id_item);
            if (clientProfile != null)
                Database.ClientProfiles.Remove(clientProfile);
        }

        public void Update(ClientProfile item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public IEnumerable<ClientProfile> AllUsers()
        {
            return Database.ClientProfiles.ToList();
        }

        public ClientProfile GetClient(int id_item)
        {
            return Database.ClientProfiles.Find(id_item);
        }
    }
}
