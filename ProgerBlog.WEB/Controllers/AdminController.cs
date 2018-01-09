using AutoMapper;
using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.BLL.Services;
using ProgerBlog.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgerBlog.WEB.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class AdminController : Controller
    {

        IPostService repo;
       
        

        public AdminController(IPostService postService)
        {
            repo = postService;
            

        }

        public ActionResult Index()
        {
            var posts = repo.GetPosts();
            
            
            return View(posts);
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(repo.GetCategories(), "Category");

            return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Create(PostEditModel post)
        {
            PostDTO postDto = Mapper.Map<PostEditModel, PostDTO>(post);

            repo.Create(postDto);

            return RedirectToAction("Index");
        }

        [HttpGet ]
        public ActionResult Edit(int? id)
        {
            ViewBag.Categories =new SelectList( repo.GetCategories(), "Category");

            if (id == null)
            {
                return HttpNotFound();
            }
            int b = (int)id;
            

            PostDTO post = repo.GetPost(b);

            

            if (post != null)
            {
                return View(post);
            }
            return HttpNotFound();
        }

        // POST: Admin/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(PostEditModel post)
        {
            PostDTO postDto = Mapper.Map<PostEditModel, PostDTO>(post);

            repo.Update(postDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            PostDTO post = repo.GetPost(id);
            if (post != null)
            {
                repo.Delete(post.Id);
            }
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            PostDTO post = repo.GetPost(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            repo.Delete(post.Id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult AddCategory(string newcategory)
        {
            repo.AddCategories(newcategory);
            
            return RedirectToAction("Index");
        }
    }
}