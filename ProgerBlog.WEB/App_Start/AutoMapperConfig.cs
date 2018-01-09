using AutoMapper;
using ProgerBlog.BLL.DTO;
using ProgerBlog.DAL.Entities;
using ProgerBlog.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgerBlog.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PostDTO, PostViewModel>();

                cfg.CreateMap<PostDTO, PostEditModel>();
                cfg.CreateMap<PostEditModel, PostDTO>();

                cfg.CreateMap<UserDTO, EditModel>();
                cfg.CreateMap<EditModel, UserDTO>();
            }); 
        }
    }
}