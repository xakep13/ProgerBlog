using AutoMapper;
using Microsoft.AspNet.Identity;
using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Infrastructure;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.DAL.Entities;
using ProgerBlog.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.BLL.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Реєстрація пройшла успішно", "");
            }
            else
            {
                return new OperationDetails(false, "Користувач з таким логіном вже існує", "Email");
            }
        }


        public async Task<OperationDetails> Delete(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                return new OperationDetails(false, "Такого користувача не снує", "");
            }
            else
            {
               // user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.DeleteAsync(user);
                
                await Database.SaveAsync();
                return new OperationDetails(true, "Видалення пройшло успішно", "");
            }
        }


        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public async Task<UserDTO> FindByNameAsync(string name)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(name);
            UserDTO userDTO = new UserDTO()
            {
                Name = user.UserName,
                Email = user.Email,
                Id = user.Id,
            };
            return userDTO;
        }
        

        public async Task<OperationDetails> UpdateAsync(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user != null)
            {    
                await Database.SaveAsync();
                return new OperationDetails(true, "Зміни збережені", "");
            }
             else return new OperationDetails(false, "Зміни не збережені", "");
        }

       
    }
}
    
