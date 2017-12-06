using ITBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITBlog.Controllers
{
    public class PostController : Controller
    {
        BlogContext db = new BlogContext();
        // GET: Post
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Post post = db.Posts.Find(id);
            if (post != null)
            {
                return View(post);
            }
            return RedirectToAction("AdminIndex", "Home");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Post post, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0 && Path.GetExtension(upload.FileName) == ".jpg")
            {
                string imgName = Path.GetFileNameWithoutExtension(upload.FileName) + "_PostId" + Guid.NewGuid().ToString() + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Files/"), imgName);
                upload.SaveAs(path);
                post.ImgPath = "/Files/" + imgName;
                if (ModelState.IsValid)
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("AdminIndex", "Home");
                }
                if (string.IsNullOrEmpty(post.Title))
                {
                    ModelState.AddModelError("Title", "Некорректный заголовок поста");
                    return RedirectToAction("Create", "Post");
                }
                if (string.IsNullOrEmpty(post.Description))
                {
                    ModelState.AddModelError("Description", "Некорректное тело поста");
                    return RedirectToAction("Create", "Post");
                }
            }
            else
            {
                ModelState.AddModelError("ImgPath", "Некорректное изображение поста");
                return RedirectToAction("Create", "Post");
            }

            return RedirectToAction("Create", "Post");
        }
        [HttpGet]
        public ActionResult Edite(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AdminIndex", "Home");
            }

            Post post = db.Posts.Find(id);
            if (post != null)
            {
                return View(post);
            }
            return RedirectToAction("AdminIndex", "Home");
        }
        [HttpPost]
        public ActionResult Edite(Post post, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0 && Path.GetExtension(upload.FileName) == ".jpg")
            {
                string imgName = Path.GetFileNameWithoutExtension(upload.FileName) + "_Id" + Guid.NewGuid().ToString() + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Files/"), imgName);
                upload.SaveAs(path);
                post.ImgPath = "/Files/" + imgName;
            }
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex", "Home");
            }
            if (string.IsNullOrEmpty(post.Title))
            {
                ModelState.AddModelError("Title", "Некорректный заголовок поста");
                return RedirectToAction("Edite", "Post", new { id = post.Id });
            }
            if (string.IsNullOrEmpty(post.Description))
            {
                ModelState.AddModelError("Description", "Некорректное тело поста");
                return RedirectToAction("Edite", "Post", new { id = post.Id });
            }

            return RedirectToAction("Edite", "Post", new { id = post.Id });
        }
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Post post = db.Posts.Find(id);
            if (post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            return RedirectToAction("AdminIndex", "Home");
        }
        public ActionResult Add()
        {
            Random random = new Random();
            Post newPost = new Post();
            for (int i = 0; i<50; i++)
            {
                
                string pathFile = @"C:\Users\Bolivar\Desktop\img\" + random.Next(1, 8) + ".jpg";
                FileInfo file = new FileInfo(pathFile);
                string imgName = Path.GetFileNameWithoutExtension(file.Name) + "_PostId" + Guid.NewGuid().ToString() + ".jpg";
                string path = Path.Combine(Server.MapPath("~/Files/"), imgName);
                file.CopyTo(path);
                newPost.ImgPath = "/Files/" + imgName;
                newPost.Title = "Пост " + (i + 24);
                newPost.Description = "Тело " + (i + 24);

                if (ModelState.IsValid)
                {
                    db.Posts.Add(newPost);
                    db.SaveChanges();
                }
            }
                
                return RedirectToAction("Index", "Home");
        }
    }
}