using ProgerBlog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        void Delete(ClientProfile item);
        void Delete(int id_item);
        void Update(ClientProfile item);
        ClientProfile GetClient(int id_item);
        IEnumerable<ClientProfile> AllUsers();
    }
}
