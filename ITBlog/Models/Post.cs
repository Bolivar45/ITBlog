using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ImgPath { get; set; }
    }
}