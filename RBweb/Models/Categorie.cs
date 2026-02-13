using System.ComponentModel.DataAnnotations;

namespace RBweb.Models
{
    public class Categorie
    {
        public int ID { get; set; }

        [Display(Name = "Nume Categorie")]
        public string Nume { get; set; }
    }
}
