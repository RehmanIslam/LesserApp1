using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using LesserApp.Models;
using Umbraco.Core.Models;
using Archetype.Models;
using Umbraco.Web;

namespace LesserApp.Controllers
{
    public class BlogSurfaceController : SurfaceController
    {
        public ActionResult RenderPostList(int numberofItems)
        {
            List<BlogPreview> model = new List<BlogPreview>();
            IPublishedContent blogpage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "blog").FirstOrDefault();
            foreach (IPublishedContent page in blogpage.Children.OrderByDescending(x => x.UpdateDate).Take(numberofItems))
            {
                int imageId = page.GetPropertyValue<int>("articleImage");
                var mediaItem = Umbraco.Media(imageId);
                
                model.Add(new BlogPreview(page.Name, page.GetPropertyValue<string>("articleIntro"), mediaItem.Url, page.Url));
            }

            return PartialView("~/Views/Partials/Blog/_PostList.cshtml",model);
        }
    }
}