using System;
using System.Collections.Generic;
using System.Text;

namespace RBmaui.Models
{
    public class Burger
    {
        public int ID { get; set; }
        public string Denumire { get; set; }
        public decimal Pret { get; set; }
        public string Imagine { get; set; }
        public string Ingrediente { get; set; }
        public int CategorieID { get; set; }
    }
}
