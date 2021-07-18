using System;
using System.ComponentModel.DataAnnotations;

namespace richmedical.Models
{
    public class Quote
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Description { get; set; }
    }
}