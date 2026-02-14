namespace RBweb.Models
{
    public class MeniuCategorie
    {
        public int ID { get; set; }

        public int MeniuID { get; set; }
        public Meniu Meniu { get; set; }

        public int CategorieID { get; set; }
        public Categorie Categorie { get; set; }
    }
}
