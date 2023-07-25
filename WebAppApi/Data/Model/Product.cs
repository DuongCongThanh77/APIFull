using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model
{
    public  class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public ICollection<OrderDetails> orderDetails { get; set; }

        public Product()
        {
            orderDetails = new List<OrderDetails>();
        }

        public int CategoryID { get; set; }
        [ForeignKey ("CategoryID")]
        public Category Category { get; set; }

    }
}
