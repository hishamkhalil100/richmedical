using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace richmedical.Models
{
    public class Specialty
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string Image { get; set; }
        public ICollection<Staff> Staffs { get; set; }

    }
}