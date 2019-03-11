using HomeWork_3_6_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork_3_6_2019.Controllers
{
    public class HomeController : Controller
    {
        BlogManager mng = new BlogManager(Properties.Settings.Default.BlogConStr);
        public ActionResult Index(int? id)
        {
            BlogIndex blogs = mng.GetNextFive(id);
            return View(blogs);
        }

        public ActionResult Blog(int id)
        {
            BlogWithCookie blog = new BlogWithCookie();
            blog.blog = mng.GetBlogById(id);
            HttpCookie cookie = Request.Cookies["Name"];
            if(cookie != null)
            {
                blog.Name = cookie.Value;
            }
            blog.NewComer = cookie == null;
            
            return View(blog);
        }

        [HttpPost]
        public ActionResult AddComment(BlogComment Bcomment)
        {
            HttpCookie cookie = new HttpCookie("Name", Bcomment.Name.ToString());
            Response.Cookies.Add(cookie);
            mng.InsertComment(Bcomment);
            return Redirect($"~/Home/Blog?id={Bcomment.Blogid}");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}