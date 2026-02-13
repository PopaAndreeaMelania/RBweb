using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Meniuri
{
    public class CreateModel : PageModel
    {
        private readonly RomanianBurgerWeb.Data.RomanianBurgerWebContext _context;

        public CreateModel(RomanianBurgerWeb.Data.RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategorieID"] = new SelectList(_context.Categorie, "ID", "Nume");
            return Page();
        }

        [BindProperty]
        public Meniu Meniu { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Meniu.Add(Meniu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
