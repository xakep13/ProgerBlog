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
        

        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose(); 
        }

        public PostDTO GetPost(int? id)
        {
            
            var post = Database.PostManager.Get(id.Value);
            if (post == null)
                
            // применяем автомаппер для проекции Phone на PhoneDTO
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<Post, PostDTO>(post);
        }

        public IEnumerable<PostDTO> GetPosts()
        {
            
            return Mapper.Map<IEnumerable<Post>, List<PostDTO>>(Database.PostManager.GetAll());
        }

        
    }
}
