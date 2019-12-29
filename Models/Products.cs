using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Models
{
    public class Products
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 5)]
        public string ProductName { get; set; }
        [Required]
        [EmailAddress]
        public string ProductType { get; set; }
        [Required]
        public int ProductVolume { get; set; }
        [Required]
        public int ProductPrice { get; set; }
    }
}
