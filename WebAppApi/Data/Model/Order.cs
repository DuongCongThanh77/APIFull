using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Order
    {
        [Key]
        public Guid OrderId { get;  set; }
        public DateTime OrderDate { get;  set; }
        public string NameOfOderer { get; set; }
        public string Email { get; set; }
        public string DeliveryLocation { get; set; }

        public ICollection<OrderDetails> orderDetails { get; set; }
        public Order()
        {
            orderDetails = new List<OrderDetails>();
        }

    }
}
