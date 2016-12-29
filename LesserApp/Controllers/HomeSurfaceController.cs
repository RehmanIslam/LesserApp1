using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Web.Mvc;
using System.Web.Mvc;
using LesserApp.Models;
using Umbraco.Web;
using Umbraco.Core.Models;
using Archetype.Models;


namespace LesserApp.Controllers
{
    public class HomeSurfaceController : SurfaceController
    {
        public ActionResult RenderFeature()
        {
            List<FeaturedItem> model = new List<FeaturedItem>();
            IPublishedContent homepage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            ArchetypeModel featuredItemss = homepage.GetPropertyValue<ArchetypeModel>("featuredItems");
            foreach(ArchetypeFieldsetModel fieldset in featuredItemss)
            {
                int imageId = fieldset.GetValue<int>("image");
                var mediaItem = Umbraco.Media(imageId);
                string imageUrl = mediaItem.Url;

                int pageId = fieldset.GetValue<int>("page");
                IPublishedContent linkedToPage = Umbraco.TypedContent(pageId);
                string linkUrl = linkedToPage.Url;

                model.Add(new FeaturedItem(fieldset.GetValue<string>("name"),fieldset.GetValue<string>("category"), imageUrl, linkUrl)); 
            }

            return PartialView("~/Views/Partials/Home/_Featured.cshtml",model);
        }
        public ActionResult RenderServices()
        {
            return PartialView("~/Views/Partials/Home/_Services.cshtml");
        }
        public ActionResult RenderBlog()
        {
            IPublishedContent homepage = CurrentPage.AncestorOrSelf("home");
            string title = homepage.GetPropertyValue<string>("latestBlogPostTitle");
            string introduction = homepage.GetPropertyValue("latestBlogPostIntroduction").ToString();

            LatestBlogPostModel model = new LatestBlogPostModel(title,introduction);
            return PartialView("~/Views/Partials/Home/_Blog.cshtml",model);
        }
        public ActionResult RenderClients()
        {
            IPublishedContent homepage = CurrentPage.AncestorOrSelf("home");
            string title = homepage.GetPropertyValue<string>("testimonialsTitle");
            string introduction = homepage.GetPropertyValue("testimonialsIntroduction").ToString();
            TestimonialsModel model = new TestimonialsModel(title, introduction);
            return PartialView("~/Views/Partials/Home/_Clients.cshtml",model);
        }
        public ActionResult RenderIntro()
        {
            return PartialView("~/Views/Partials/SiteLayout/_intro.cshtml");
        }
    }
}