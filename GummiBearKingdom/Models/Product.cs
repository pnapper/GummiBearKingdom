using System;
using System.ComponentModel.DataAnnotations;

namespace GummiBearKingdom.Models
{

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
