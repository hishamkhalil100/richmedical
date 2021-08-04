using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace richmedical.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String NameAr { get; set; }
        public String Image { get; set; }
        public string Description { get; set; }
        public string DescriptionAr { get; set; }



    }
}