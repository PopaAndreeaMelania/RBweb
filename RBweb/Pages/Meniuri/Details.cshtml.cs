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
    public class DetailsModel : PageModel
    {
        private readonly RomanianBurgerWeb.Data.RomanianBurgerWebContext _context;

        public DetailsModel(RomanianBurgerWeb.Data.RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public Meniu Meniu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meniu = await _context.Meniu.FirstOrDefaultAsync(m => m.ID == id);

            if (meniu is not null)
            {
                Meniu = meniu;

                return Page();
            }

            return NotFound();
        }
    }
}
