using System;
using System.ComponentModel.DataAnnotations;

namespace richmedical.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameAr { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }
    }
}