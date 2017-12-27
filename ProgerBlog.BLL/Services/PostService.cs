using ProgerBlog.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgerBlog.BLL.DTO;
using ProgerBlog.DAL.UnitOfWork;
using AutoMapper;
using ProgerBlog.DAL.Entities;

namespace ProgerBlog.BLL.Services
{
    public class PostService : IPostService
    {
        IUnitOfWork Database { get; set; }
        static List<string> categories = new List<string> { "Кулінарія","Програмування"};

        


        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose(); 
        }
        public List<string> GetCategories()
        {
            return categories;
        }
        public void AddCategories(string s)
        {
            categories.Add(s);
        }

        public PostDTO GetPost(int? id)
        {
            
            var post = Database.PostManager.Get(id.Value);
            
            // применяем автомаппер для проекции Phone на PhoneDTO
           
            return Mapper.Map<Post, PostDTO>(post);
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            
            return Mapper.Map<IEnumerable<Post>, List<PostDTO>>(Database.PostManager.GetAll());
        }

        public void Create(PostDTO post)
        {
            Post _post = Mapper.Map<Post>(post);
            Database.PostManager.Create(_post);
        }

        public void Update(PostDTO post)
        {

            Post _post = Mapper.Map<Post>(post);
            Database.PostManager.Update(_post);
        }

        public void Delete(int? id_item)
        {
            Post post = Database.PostManager.Get(id_item);
            if (post != null)
                Database.PostManager.Delete(post);
        }

        
    }
}
