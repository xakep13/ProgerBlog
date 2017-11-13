﻿using AutoMapper;
using ProgerBlog.BLL.DTO;
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
            }); 
        }
    }
}