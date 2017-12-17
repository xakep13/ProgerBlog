using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgerBlog.WEB.Controllers
{
    public class UserController : Controller
    {
        private List<string> Roles = new List<string>
        {
            "admin",
            "user",
            "moderator"
        };

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var users = Mapper.Map<List<UserDTO>, List<EditModel>>(UserService.GetUsers().ToList());

            return View(users);
        }


        [HttpGet]
        public ActionResult Delete(string id)
        {

            UserService.Delete(id);
            UserService.UpdateAsync();

            return RedirectToAction("Index", "User");
        }



        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(string id)
        {


            UserService.Delete(id);
            UserService.UpdateAsync();

            return RedirectToAction("Index", "User");
        }


        [HttpGet]
        public ActionResult Edit(string id)
        {
            UserDTO user = UserService.GetUser(id);

            EditModel model = Mapper.Map<UserDTO, EditModel>(user);



            ViewBag.Roles = new SelectList(Roles, "Roles");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditModel model)
        {
            UserDTO user = Mapper.Map<EditModel, UserDTO>(model);

            UserService.UpdateAsync(user);

            return RedirectToAction("Index", "User");
        }

    }
}