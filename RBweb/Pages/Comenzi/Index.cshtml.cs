using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Comenzi
{
    public class IndexModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public IndexModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comanda { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Comanda = await _context.Comanda
                .Include(c => c.Items)
                .OrderByDescending(c => c.DataComanda)
                .ToListAsync();
        }
    }
}
