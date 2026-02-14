using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Comenzi
{
    public class MeniuModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public MeniuModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Meniu> Meniuri { get; set; } = new List<Meniu>();

        public async Task OnGetAsync()
        {
            Meniuri = await _context.Meniu
                .Include(m => m.MeniuCategorii)
                    .ThenInclude(mc => mc.Categorie)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
