using Microsoft.AspNet.Identity.EntityFramework;
using ProgerBlog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgerBlog.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationContext()
        {
            Database.SetInitializer<ApplicationContext>(new MyContextInitializer());
        }

        public ApplicationContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
    class MyContextInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext db)
        {
            Post e1 = new Post {SubTitle = "Підзаголовок", Category = "Програмування", Date = new DateTime(2008, 5, 1, 8, 30, 52), Title = "Знакомимся с iCEstick и полностью открытым ПО для разработки под FPGA", Author = "Programer", Text = "Есть проект IceStorm, который ставит перед собой целью реверс-инжиниринг и документирование внутреннего устройства FPGA серии iCE40 от компании Lattice, а также предоставление открытого ПО для разработки под эти FPGA. С этим проектом тесно связаны утилиты Yosys и Arachne-pnr, предназначенные, собственно, для компиляции кода на Verilog или SystemVerilog. Первая утилита выполняет синтез, а вторая — имплементацию, а точнее, один из шагов имплементации под названием place and route. Наконец, конфигурирование FGPA выполняется с помощью утилит icepack и iceprog, являющихся частью пакета IceStorm." };
            Post e2 = new Post {SubTitle = "Підзаголовок", Category = "Кулінарія", Date = new DateTime(2008, 5, 1, 8, 30, 52), Title = "Об RFID-метках и работу с ними при помощи Arduino", Text = "Вам, конечно же, знакомы карточки и брелки, которые нужно подносить к считывателю, а они при этом пропускают на работу или дают проехать в метро. Такие брелки и карточки используют технологию под названием RFID, Radio Frequency IDentification. Сегодня мы познакомимся с основами этой технологии, а также узнаем, как использовать ее в своих проектах на базе Arduino.Работа RFID неплохо расписана в соответствующей статье на Википедии. Если в двух словах, большую часть карточки или брелка, которые далее мы будем называть RFID-метками, занимает антенна. Также в метке содержится очень маленький чип, реализующий всю логику. В ридере также есть антенна, притом регулярно передающая сигнал, и следовательно создающая электромагнитное поле. При поднесении метки в это поле на ее антенне индуцируется ток, который и питает метку. Теперь метка и ридер могут пообщаться друг с другом при помощи радиосигнала, используя какой-то свой протокол и модуляцию сигнала.", Author = "proger" };
            db.Posts.Add(e1);
            db.Posts.Add(e2);
            db.SaveChanges();
            base.Seed(db);
        }
    }
}
