using System.ComponentModel.DataAnnotations;

namespace RBweb.Models
{
    public class Comanda
    {
        public int ID { get; set; }

        [Required]
        public string UserEmail { get; set; } = "";

        public DateTime DataComanda { get; set; } = DateTime.Now;

        public ICollection<ComandaItem> Items { get; set; } = new List<ComandaItem>();
        public string? Mentiuni { get; set; }
    }
}
