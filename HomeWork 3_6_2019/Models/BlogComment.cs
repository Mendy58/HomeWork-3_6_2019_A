using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork_3_6_2019.Models
{
    public class BlogComment
    {
        public int id { get; set; }
        public int Blogid { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
    }
}