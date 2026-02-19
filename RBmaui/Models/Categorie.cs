using System;
using System.Collections.Generic;
using System.Text;

namespace RBmaui.Models
{
    public class Categorie
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<Burger> Burgeri { get; set; }
    }
}
