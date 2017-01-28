using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        public int Type { get; set; }
        //public string Type {get;set;}

        public virtual Product Product { get; set; }
    }
}