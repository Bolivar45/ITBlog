using ITBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace ITBlog.Controllers
{
    public class HomeController : Controller
    {
        BlogContext db = new BlogContext();
        public ActionResult Index(int? page)
        {
            List<Post> posts = db.Posts.ToList();
            int pageNumber = page ?? 1;
            posts.Reverse();
            var onePageOfPosts = posts.ToPagedList(pageNumber, 3);

            ViewBag.onePageOfPosts = onePageOfPosts;

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AdminIndex(int? page)
        {
            List<Post> posts = db.Posts.ToList();
            int pageNumber = page ?? 1;
            posts.Reverse();
            var onePageOfPosts = posts.ToPagedList(pageNumber, 3);

            ViewBag.onePageOfPosts = onePageOfPosts;

            return View();
           
        }
    }
}