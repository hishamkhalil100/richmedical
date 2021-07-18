using System;

namespace richmedical.Models
{
    public class TeamMember
    {
        public Guid Id { get; set; }
        public string Name{ get; set; }
        public string NameAr{ get; set; }
        public string Image { get; set; }
        public string Description{ get; set; }
        public string DescriptionAr{ get; set; }

    }
}