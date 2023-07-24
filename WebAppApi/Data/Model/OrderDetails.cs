using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
  
        public Guid ProductID { get; set; }
        public int NumberOfProduct { get; set; }

        // relationShip
        public Order order { get; set; }
        public Product product { get; set; }
    }
}
