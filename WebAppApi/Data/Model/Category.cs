using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Model
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }


    }
}
