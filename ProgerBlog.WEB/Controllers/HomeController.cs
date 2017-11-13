using AutoMapper;
using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.WEB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgerBlog.WEB.Controllers
{
    public class HomeController : Controller
    {
        IPostService postService;
        public HomeController(IPostService serv)
        {
            postService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<PostDTO> postDtos = postService.GetPosts();
           
            var posts = Mapper.Map<IEnumerable<PostDTO>, List<PostViewModel>>(postDtos);

            return View(posts);
        }

        public ActionResult Post(int? id)
        {
            try
            {
                PostDTO post = postService.GetPost(id);              
                return View(post);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            postService.Dispose();
            base.Dispose(disposing);
        }
    }
}