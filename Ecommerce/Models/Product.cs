﻿using System;
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
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        public string ProductImage { get; set; }

        public virtual Supplier Supplier { get; set; }

    }
}