using System.ComponentModel.DataAnnotations;

namespace RBweb.Models
{
    public class Comanda
    {
        public int ID { get; set; }

        public int? ClientID { get; set; }
        public Client? Client { get; set; }

        public int? MeniuID { get; set; }
        public Meniu? Meniu { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataLivrare { get; set; }
    }
}
