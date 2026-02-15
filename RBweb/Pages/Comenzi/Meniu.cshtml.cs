using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

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
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
