using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LesserApp.Models
{
    public class NavigationListItems
    {
        public string Text { get; set; }
        public NavigationLinks Links { get; set; }
        public List<NavigationListItems> Items { get; set; }
        public bool HasChildren { get { return Items != null && Items.Any() && Items.Count > 0; } }
        public NavigationListItems() { }
        public NavigationListItems(NavigationLinks link)
        {
            Links = link;
        }
        public NavigationListItems(string text)
        {
            Text = text;
        }
    }
}