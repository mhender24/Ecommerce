using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [Required]
        [ForeignKey("ProductDetail")]
        public int ProductDetailId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ProductDetail ProductDetail { get; set; }

    }
}