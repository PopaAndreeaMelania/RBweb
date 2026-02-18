using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Comenzi
{
    [Authorize]
    public class ComenzileMeleModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public ComenzileMeleModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Comanda> Comenzi { get; set; } = new List<Comanda>();

        public async Task OnGetAsync()
        {
            var email = User.Identity?.Name;

            Comenzi = await _context.Comanda
                .Where(c => c.UserEmail == email)
                .OrderByDescending(c => c.DataComanda)
                .ToListAsync();
        }
    }
}
