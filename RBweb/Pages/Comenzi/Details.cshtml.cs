using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Comenzi
{
    public class DetailsModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public DetailsModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public Comanda Comanda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var comanda = await _context.Comanda
                .Include(c => c.Items)
                    .ThenInclude(i => i.Meniu)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (comanda == null) return NotFound();

            Comanda = comanda;
            return Page();
        }
    }
}
