using RBweb.Models;

namespace RBweb.ViewModels
{
    public class CategorieIndexData
    {
        public IEnumerable<Categorie> Categorii { get; set; }
        public IEnumerable<Meniu> Meniuri { get; set; }
    }
}
