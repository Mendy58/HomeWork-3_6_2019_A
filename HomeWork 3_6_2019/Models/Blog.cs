using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork_3_6_2019.Models
{
    public class Blog
    {
        public Blog()
        {
            Comments = new List<BlogComment>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Boichvei { get; set; }
        public List<BlogComment> Comments { get; set; }
    }
}