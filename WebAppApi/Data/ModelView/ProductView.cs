using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelView
{
    public class ProductView
    {
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public int CategoryID { get; set; }
    }
}
