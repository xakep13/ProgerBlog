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
        protected MapperConfiguration mapper;

        public UserService(IUnitOfWork uow)
        {
            Database = uow;

            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDTO>()
                    .ForMember("Name", otp => otp.MapFrom(c => c.ClientProfile.Name))
                    .ForMember("Role", otp => otp.MapFrom(c => String.Join(",", Database.UserManager.GetRoles(c.Id))))
                    .ForMember("IsDelete", otp => otp.MapFrom(c => c.ClientProfile.IsDelete))
                    .ForMember("Password", otp => otp.MapFrom(c => c.PasswordHash))
                    .ForMember("Address", otp => otp.MapFrom(c => c.ClientProfile.Address));
                cfg.CreateMap<UserDTO, ApplicationUser>()
                    .ForPath(dest => dest.ClientProfile.Name, opt => opt.MapFrom(src => src.Name))
                    .ForPath(dest => dest.ClientProfile.Address, opt => opt.MapFrom(src => src.Address))
                    .ForPath(dest => dest.ClientProfile.IsDelete, opt => opt.MapFrom(src => src.IsDelete))
                    .ForPath(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                    .ForPath(dest => dest.Roles, opt => opt.MapFrom(src => src.Role));

                ;

            });

        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email,  };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name, IsDelete = userDto.IsDelete };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Реєстрація пройшла успішно", "");
            }
            else
            {
                return new OperationDetails(false, "Користувач з таким логіном вже існує", "Email");
            }
        }


        


        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity

            if (user != null)
                if (user.ClientProfile.IsDelete == false)
                    claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.GetRoleManager().FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.GetRoleManager().CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
        }       

        public async Task<OperationDetails> UpdateAsync(UserDTO userDto)
        {    
            ApplicationUser user =  Database.UserManager.FindById(userDto.Id);
          
            if (user != null)
            {
                IList<string> roles = Database.UserManager.GetRoles(user.Id);
                 Database.UserManager.RemoveFromRoles(user.Id, String.Join(",",roles));
                 Database.UserManager.AddToRole(user.Id, userDto.Role);

                user.ClientProfile.Name = userDto.Name;
                user.Email = userDto.Email;

                await Database.SaveAsync();
                return new OperationDetails(true, "Зміни збережені", "");
            }
             else return new OperationDetails(false, "Зміни не збережені", "");
        }
        public async Task<OperationDetails> UpdateAsync()
        {
            await Database.SaveAsync();

            return new OperationDetails(true, "Зміни збережені", "");
        }
   

        public OperationDetails Delete(string id)
        {
            ApplicationUser user = Database.UserManager.FindById(id);

            if (user != null)
            {
                if(!user.ClientProfile.IsDelete)
                    user.ClientProfile.IsDelete = true;
                else
                    user.ClientProfile.IsDelete = false;
                Database.SaveAsync();

                return new OperationDetails(true, "Видалення відбулось", "");
            }

            return new OperationDetails(false, "Видалення не відюулось", "");
        }


        public UserDTO GetUser(string id)
        {
            var map = mapper.CreateMapper();
            ApplicationUser user = Database.UserManager.FindById(id);

            return map.Map<ApplicationUser, UserDTO>(user);

        }

        public List<UserDTO> GetUsers()
        {
            var map = mapper.CreateMapper();
            List<UserDTO> users = map.Map<List<ApplicationUser>, List<UserDTO>>(Database.UserManager.Users.ToList());

            return users;
        }
    }
}
    
