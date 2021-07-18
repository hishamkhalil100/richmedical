using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace richmedical.Models
{
    public class ContactUs
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
}