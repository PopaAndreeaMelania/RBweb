using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Meniuri
{
    public class IndexModel : PageModel
    {
        private readonly RomanianBurgerWeb.Data.RomanianBurgerWebContext _context;

        public IndexModel(RomanianBurgerWeb.Data.RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IList<Meniu> Meniu { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Meniu = await _context.Meniu
                .Include(m => m.Categorie)
                .ToListAsync();
        }
    }
}
