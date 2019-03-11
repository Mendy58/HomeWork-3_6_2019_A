using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork_3_6_2019.Models
{
    public class BlogIndex
    {
        public BlogIndex()
        {
            blogs = new List<Blog>();
        }
        public List<Blog> blogs { get; set; }
        public int PreviousIndex { get; set; }
        public int NextIndex { get; set; }
        public int LastId { get; set; }
        public int FirstId { get; set; }
    }
}