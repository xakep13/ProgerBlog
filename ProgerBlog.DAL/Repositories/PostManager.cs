﻿using ProgerBlog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgerBlog.DAL.Entities;
using ProgerBlog.DAL.Context;
using System.Data.Entity;

namespace ProgerBlog.DAL.Repositories
{
    public class PostManager : IPostManager
    {
        public ApplicationContext Database { get; set; }
        public PostManager(ApplicationContext db)
        {
            Database = db;
        }

        public void Create(Post item)
        {
            Database.Posts.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Delete(Post item)
        {
            Post post = Database.Posts.Find(item);
            if (post != null)
                Database.Posts.Remove(post);
        }

        public void Update(Post item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id_item)
        {
            Post post = Database.Posts.Find(id_item);
            if (post != null)
                Database.Posts.Remove(post);
        }

        public IEnumerable<Post> GetAll()
        {
           return Database.Posts.ToList();
        }

        public Post Get(int id_item)
        {
            return Database.Posts.FirstOrDefault(p => p.Id == id_item);
        }
    }
}
