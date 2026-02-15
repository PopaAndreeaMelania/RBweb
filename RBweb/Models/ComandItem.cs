using System.ComponentModel.DataAnnotations;

namespace RBweb.Models
{
    public class ComandaItem
    {
        public int ID { get; set; }

        public int ComandaID { get; set; }
        public Comanda? Comanda { get; set; }

        public int MeniuID { get; set; }
        public Meniu? Meniu { get; set; }

        [Range(1, 100)]
        public int Cantitate { get; set; } = 1;

        public decimal Pret { get; set; }
    }
}
