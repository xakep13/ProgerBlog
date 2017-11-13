using Ninject.Modules;
using ProgerBlog.BLL.Interfaces;
using ProgerBlog.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgerBlog.WEB.Util
{
    public class PostModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IPostService>().To<PostService>();
        }
    }
}