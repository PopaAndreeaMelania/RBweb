using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Admin.Comenzi
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public IndexModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comenzi { get; set; } = new List<Comanda>();

        public async Task OnGetAsync()
        {
            Comenzi = await _context.Comanda
                .OrderByDescending(c => c.DataComanda)
                .ToListAsync();
        }
    }
}
