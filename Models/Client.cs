using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace richmedical.Models
{
    public class Client
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