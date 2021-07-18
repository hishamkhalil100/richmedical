using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace richmedical.Models
{
    public class News
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string DescriptionAr { get; set; }

        public DateTime CreationDate { get; set; }
    }
}