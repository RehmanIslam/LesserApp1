using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LesserApp.Models
{
    public class LatestBlogPostModel
    {
        public string Title { get; set; }
        public string Introduction { get; set; }
        public LatestBlogPostModel(string title,string introduction)
        {
            Title = title;
            Introduction = introduction;
        }
    }
}