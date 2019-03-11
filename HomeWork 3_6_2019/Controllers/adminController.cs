using HomeWork_3_6_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork_3_6_2019.Controllers
{
    public class adminController : Controller
    {
        BlogManager mng = new BlogManager(Properties.Settings.Default.BlogConStr);
        // GET: admin
        public ActionResult Add()
        {
            return View();
        }

        //public ActionResult DeleteBlog()
        //{

        //}

        [HttpPost]
        public ActionResult AddBlog(Blog blog)
        {
            int Scope = mng.InsertBlog(blog);
            return Redirect($"~/Home/Blog?id={Scope}");
        }
    }
}