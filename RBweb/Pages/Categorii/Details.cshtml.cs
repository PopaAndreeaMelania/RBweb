using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Categorii
{
    public class DetailsModel : PageModel
    {
        private readonly RomanianBurgerWeb.Data.RomanianBurgerWebContext _context;

        public DetailsModel(RomanianBurgerWeb.Data.RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public Categorie Categorie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie.FirstOrDefaultAsync(m => m.ID == id);

            if (categorie is not null)
            {
                Categorie = categorie;

                return Page();
            }

            return NotFound();
        }
    }
}
