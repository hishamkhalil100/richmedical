using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace richmedical.Models
{
    public class Staff
    {
        public Guid Id { get; set; }
        public  string Name{ get; set; }
        public  string NameAr{ get; set; }
        public  string Image{ get; set; }
        public  string Description{ get; set; }
        public  string DescriptionAr{ get; set; }
        public  Guid SpecialtyId { get; set; }
        public  Specialty Specialty { get; set; }
        
        
    }
}