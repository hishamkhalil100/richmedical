using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using richmedical.Models;

namespace richmedical.ViewModel
{
    public class HomeViewModel
    {
        public ICollection<TeamMember> teamMembers { get; set; }
        public ICollection<Client> Clients { get; set; }
        public ICollection<News> News { get; set; }
    }
}