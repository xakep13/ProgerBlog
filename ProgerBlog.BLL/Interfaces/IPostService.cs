using ProgerBlog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.BLL.Interfaces
{
    public interface IPostService 
    {
        PostDTO GetPost(int? id);
        IEnumerable<PostDTO> GetPosts();
        void Dispose();
    }
}
