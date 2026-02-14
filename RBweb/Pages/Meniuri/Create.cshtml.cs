using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Meniuri
{
    public class CreateModel : MeniuCategoriiPageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public CreateModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meniu Meniu { get; set; } = default!;

        public IActionResult OnGet()
        {
            // IMPORTANT: setează proprietatea Meniu (nu doar o variabilă locală)
            Meniu = new Meniu
            {
                MeniuCategorii = new List<MeniuCategorie>()
            };

            PopulateAssignedCategorieData(_context, Meniu);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategorii)
        {
            var newMeniu = new Meniu();

            // Legăm DOAR câmpurile pe care vrem să le acceptăm din form
            if (await TryUpdateModelAsync(
                newMeniu,
                "Meniu",
                m => m.Denumire,
                m => m.Pret,
                m => m.Imagine,
                m => m.Ingrediente))
            {
                newMeniu.DataAdaugare = DateTime.Now;

                // many-to-many (checkbox-uri)
                newMeniu.MeniuCategorii = new List<MeniuCategorie>();
                if (selectedCategorii != null)
                {
                    foreach (var cat in selectedCategorii)
                    {
                        newMeniu.MeniuCategorii.Add(new MeniuCategorie
                        {
                            CategorieID = int.Parse(cat)
                        });
                    }
                }

                _context.Meniu.Add(newMeniu);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // Dacă validarea pică, refacem checkbox-urile
            PopulateAssignedCategorieData(_context, newMeniu);
            return Page();
        }
    }
}
