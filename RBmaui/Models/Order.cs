using System;
using System.Collections.Generic;
using System.Text;

namespace RBmaui.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
