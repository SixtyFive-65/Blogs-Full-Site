﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogz.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool IsVisible { get; set; }

        //Display Tags
        public IEnumerable<SelectListItem> Tags { get; set; } // Tags populates SelectedTag

        //Collect Tag - retreive selected tag
        //public string SelectedTag {  get; set; }   //Populated by Tags // Collects a single tag at a time

        //Collect Tag - retreive selected tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();  //Populated by Tags // Collects a list of tagIds
    }
}