using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ProductDetail
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ProductImage { get; set; }
    }
}