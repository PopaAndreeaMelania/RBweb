using System;
using System.Collections.Generic;

namespace RBmaui.Models
{
    public class Comanda
    {
        public int ID { get; set; }
        public string CodComanda { get; set; }
        public int NumarOrdine { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public string EmailClient { get; set; }
        public decimal Total { get; set; }
        public List<Order> Detalii { get; set; } = new List<Order>();
    }
}