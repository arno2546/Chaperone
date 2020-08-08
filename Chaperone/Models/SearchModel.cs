using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chaperone.Models
{
    public class SearchModel
    {
        [Required]
        public string searchString { get; set; }
        public bool festival { get; set; }
        public bool sports { get; set; }
        public bool food { get; set; }
        public bool nightlife { get; set; }
        public bool culture { get; set; }
        public bool Male { get; set; }
        public bool Female { get; set; }
        public bool English { get; set; }
        public bool Spanish { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}