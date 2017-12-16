using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.BLL.Services;
using System;
using System.Collections.Generic;
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
            var posts = repo.GetPosts();
            var categories = from u in posts select u.Category;
            ViewBag.Categories = new SelectList(categories, "Category");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostDTO post)
        {
            repo.Create(post);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var posts = repo.GetPosts();
            var categories = from u in posts select u.Category;
            ViewBag.Categories =new SelectList( categories,"Category");

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
        [HttpPost]
        public ActionResult Edit(PostDTO post)
        {
            repo.Update(post);

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
    }
}