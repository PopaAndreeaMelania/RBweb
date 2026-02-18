using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Admin.Comenzi
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public DetailsModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public Comanda Comanda { get; set; } = default!;
        public decimal Total => Comanda.Items.Sum(i => (i.Meniu?.Pret ?? 0m) * i.Cantitate);

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var comanda = await _context.Comanda
                .Include(c => c.Items)
                    .ThenInclude(i => i.Meniu)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (comanda == null) return NotFound();

            Comanda = comanda;
            return Page();
        }
    }
}
