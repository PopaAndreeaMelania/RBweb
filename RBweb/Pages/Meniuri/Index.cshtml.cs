using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Meniuri
{
    public class IndexModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public IndexModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Meniu> Meniu { get; set; } = default!;

        // ca in Lab 4: TitleSort / AuthorSort / CurrentFilter :contentReference[oaicite:2]{index=2}
        public string DenumireSort { get; set; }
        public string PretSort { get; set; }
        public string CurrentFilter { get; set; }

        // ca in Lab 4: OnGetAsync(... sortOrder, searchString) :contentReference[oaicite:3]{index=3}
        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            DenumireSort = String.IsNullOrEmpty(sortOrder) ? "den_desc" : "";
            PretSort = sortOrder == "pret" ? "pret_desc" : "pret";
            CurrentFilter = searchString;

            var query = _context.Meniu
                .Include(m => m.MeniuCategorii)
                    .ThenInclude(mc => mc.Categorie)
                .AsNoTracking();

            // filtrare ca in Lab 4 (searchString) :contentReference[oaicite:4]{index=4}
            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(m =>
                    m.Denumire.Contains(searchString) ||
                    m.MeniuCategorii.Any(mc => mc.Categorie.CategoryName.Contains(searchString)));
            }

            // sortare ca in Lab 4 (switch sortOrder) :contentReference[oaicite:5]{index=5}
            query = sortOrder switch
            {
                "den_desc" => query.OrderByDescending(m => m.Denumire),
                "pret" => query.OrderBy(m => m.Pret),
                "pret_desc" => query.OrderByDescending(m => m.Pret),
                _ => query.OrderBy(m => m.Denumire)
            };

            Meniu = await query.ToListAsync();
        }
    }
}
