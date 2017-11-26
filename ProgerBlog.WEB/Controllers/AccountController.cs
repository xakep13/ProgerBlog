using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Infrastructure;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProgerBlog.WEB.Controllers
{
    public class AccountController : Controller
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



        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Невірний логін або пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "unopcpavilion@gmail.com",
                UserName = "unopcpavilion@gmail.com",
                Password = "12345678",
                Name = "Чорнобай Сергій Васильович",
                Address = "вул. Кам'янецька 112/2, кв. 146",
                Role = "admin",
            }, Roles);

            await UserService.SetInitialData(new UserDTO
            {
                Email = "uno@gmail.com",
                UserName = "uno@gmail.com",
                Password = "12345678",
                Name = "Чорнобай Сергій Васильович",
                Address = "вул. Кам'янецька 112/2, кв. 146",
                Role = "user",
            }, Roles);

            await UserService.SetInitialData(new UserDTO
            {
                Email = "pavilion@gmail.com",
                UserName = "cpavilion@gmail.com",
                Password = "12345678",
                Name = "Чорнобай Сергій Васильович",
                Address = "вул. Кам'янецька 112/2, кв. 146",
                Role = "moderator",
            }, Roles);
        }



        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            UserDTO user = await UserService.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                OperationDetails result = await UserService.Delete(user);
                if (result.Succedeed)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Edit()
        {
            UserDTO user = await UserService.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { Name = user.Name };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            UserDTO user = await UserService.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                user.Name = model.Name;
                OperationDetails result = await UserService.UpdateAsync(user);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }
    }
}