using ProgerBlog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.Interfaces
{
    public interface IPostManager: IDisposable
    {
        void Create(Post item);
        void Delete(Post item);
        void Update(Post item);
        void Delete(int? id_item);
        IEnumerable<Post> GetAll();
        Post Get(int? id_item);
        
    }
}
