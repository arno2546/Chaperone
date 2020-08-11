using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Chaperone.Models
{
    public class PayModel
    {
        [Required]
        public string AccountNo { get; set; }
        [Required]
        public float Amount { get; set; }
        [Required]
        public string Password { get; set; }
    }
}