using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBweb.Models
{
    public class Categorie
    {
        public int ID { get; set; }

        [Display(Name = "Nume Categorie")]
        [Column("Nume")]   // 🔥 asta rezolvă problema
        public string CategoryName { get; set; } = string.Empty;

        public ICollection<MeniuCategorie> MeniuCategorii { get; set; } = new List<MeniuCategorie>();
    }
}
