using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;
using RBweb.ViewModels;

namespace RBweb.Pages.Categorii
{
    public class IndexModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public IndexModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public CategorieIndexData CategorieData { get; set; }
        public int CategorieID { get; set; }

        public async Task OnGetAsync(int? id)
        {
            CategorieData = new CategorieIndexData();

            CategorieData.Categorii = await _context.Categorie
                .Include(c => c.MeniuCategorii)
                    .ThenInclude(mc => mc.Meniu)
                .AsNoTracking()
                .OrderBy(c => c.CategoryName)
                .ToListAsync();

            if (id != null)
            {
                CategorieID = id.Value;

                var categorie = CategorieData.Categorii
                    .Single(c => c.ID == id.Value);

                CategorieData.Meniuri = categorie.MeniuCategorii
                    .Select(mc => mc.Meniu);
            }
        }
    }
}
