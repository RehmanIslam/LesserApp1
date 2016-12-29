using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LesserApp.Models
{
    public class BlogPreview
    {
        public string Name { get; set; }
        public string Introduction { get; set; }
        public string ImageUrl { get; set; }
        public string LinkedUrl { get; set; }

        public BlogPreview(string name, string introduction, string imageUrl, string linkUrl)
        {
            Name = name;
            Introduction = introduction;
            ImageUrl = imageUrl;
            LinkedUrl = linkUrl;
        }
    }
}