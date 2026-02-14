using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RBweb.Models
{
    public class Meniu
    {
        public int ID { get; set; }

        [Display(Name = "Denumire Produs")]
        public string Denumire { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        [Display(Name = "Pret")]
        public decimal Pret { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataAdaugare { get; set; }

        public ICollection<MeniuCategorie>? MeniuCategorii { get; set; }
        public ICollection<Comanda>? Comenzi { get; set; }
        public string? Imagine { get; set; }

        public string? Ingrediente { get; set; }

    }
}
