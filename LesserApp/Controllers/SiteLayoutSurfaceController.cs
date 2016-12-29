using LesserApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using System.Runtime.Caching;
using Umbraco.Web;
namespace LesserApp.Controllers
{
    public class SiteLayoutSurfaceController : SurfaceController
    {
        //public const string PARTIAL_VIEW_FOLDER = "~/Views/Partials/SiteLayout /";
        
        public ActionResult RenderFooter()
        {
            return PartialView("~/Views/Partials/SiteLayout/_Footer.cshtml");
        }
        public ActionResult RenderTitleControls()
        {
            return PartialView("~/Views/Partials/SiteLayout/_TitleControls.cshtml");
        }
        ///This is LesserApp
        /// <summary>
        /// Renders the top navigation partial
        /// </summary>
        /// <returns>Partial view with a model</returns>
        public ActionResult RenderHeader()
        {
            List<NavigationListItems> nav = GetNavigationModelFromDatabase();/*<List<NavigationListItems>>("mainNav", 0, GetNavigationModelFromDatabase);*/
            return PartialView("~/Views/Partials/SiteLayout/_Header.cshtml",nav);
        }
        /// <summary>
        /// Finds the home page and gets the navigation structure based on it and it's children
        /// </summary>
        /// <returns>A List of NavigationListItems, representing the structure of the site.</returns>
        private List<NavigationListItems> GetNavigationModelFromDatabase()
        {
            IPublishedContent homePage = CurrentPage.AncestorOrSelf(1).DescendantsOrSelf().Where(x => x.DocumentTypeAlias == "home").FirstOrDefault();
            List<NavigationListItems> nav = new List<NavigationListItems>();
            nav.Add(new NavigationListItems(new NavigationLinks(homePage.Url,homePage.Name)));
            nav.AddRange(GetChildNavigationList(homePage));
            return nav; 
        }
        /// <summary>
        /// Loops through the child pages of a given page and their children to get the structure of the site.
        /// </summary>
        /// <param name="page">The parent page which you want the child structure for</param>
        /// <returns>A List of NavigationListItems, representing the structure of the pages below a page.</returns>
        private List<NavigationListItems> GetChildNavigationList(IPublishedContent page)
        {
            List<NavigationListItems> listItems = null;
            var ChildPages = page.Children.Where("Visible").Where(x => x.Level <=2).Where(x => !x.HasValue("excludeFromTopNavigation") 
            || (x.HasValue("excludeFromTopNavigation") 
            && x.GetPropertyValue<bool>("excludeFromTopNavigation")));
            if (ChildPages != null && ChildPages.Any() && ChildPages.Count() > 0)
            {
                listItems = new List<NavigationListItems>();
                foreach(var childpage in ChildPages)
                {
                    NavigationListItems listItem = new NavigationListItems(new NavigationLinks(childpage.Url,childpage.Name));
                    listItem.Items = GetChildNavigationList(childpage);
                    listItems.Add(listItem);
                }
            }
            return listItems;
        }
        /// <summary>
        /// A generic function for getting and setting objects to the memory cache.
        /// </summary>
        /// <typeparam name="T">The type of the object to be returned.</typeparam>
        /// <param name="cacheItemName">The name to be used when storing this object in the cache.</param>
        /// <param name="cacheTimeInMinutes">How long to cache this object for.</param>
        /// <param name="objectSettingFunction">A parameterless function to call if the object isn't in the cache and you need to set it.</param>
        /// <returns>An object of the type you asked for</returns>
        //private static T GetObjectFromCache<T>(string cacheItemName, int cacheTimeInMinutes, Func<T> objectSettingFunction)
        //{
        //    ObjectCache cache = MemoryCache.Default;
        //    var cachedObject = (T)cache[cacheItemName];
        //    if (cachedObject == null)
        //    {
        //        CacheItemPolicy policy = new CacheItemPolicy();
        //        policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheTimeInMinutes);
        //        cachedObject = objectSettingFunction();
        //        cache.Set(cacheItemName, cachedObject, policy);
        //    }
        //    return cachedObject;
        //}
    }
}