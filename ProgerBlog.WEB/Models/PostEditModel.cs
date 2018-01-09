using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgerBlog.WEB.Models
{
    public class PostEditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть заголовок")]
        public string Title { get; set; }
        
        public string SubTitle { get; set; }
        [Required(ErrorMessage = "Введіть текст")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Введіть автора")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Введіть категорію")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Введіть дату")]
        public DateTime Date { get; set; }
    }
}