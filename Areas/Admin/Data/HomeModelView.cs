using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace richmedical.Areas.Admin.Data
{
    public class HomeModelView
    {
        public int Clients { get; set; }
        public int Activities { get; set; }
        public int Committees { get; set; }
        public int News { get; set; }
        public int Specialties { get; set; }
        public int Staffs { get; set; }
        public int TeamMembers { get; set; }
    }
}