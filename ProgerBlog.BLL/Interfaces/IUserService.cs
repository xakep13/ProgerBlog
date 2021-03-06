﻿using ProgerBlog.BLL.DTO;
using ProgerBlog.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace ProgerBlog.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);

        OperationDetails Delete(string id);

        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        List<UserDTO> GetUsers();
        UserDTO GetUser(string name);
       
        Task<OperationDetails> UpdateAsync(UserDTO user);
        Task<OperationDetails> UpdateAsync();


    }
}
