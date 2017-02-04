using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Customer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Phone { get; set; }
      
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}