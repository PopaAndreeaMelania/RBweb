using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.Pages.Meniuri
{
    public class EditModel : MeniuCategoriiPageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public EditModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meniu Meniu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Meniu = await _context.Meniu
                .Include(m => m.MeniuCategorii)
                    .ThenInclude(mc => mc.Categorie)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Meniu == null)
                return NotFound();

            PopulateAssignedCategorieData(_context, Meniu);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategorii)
        {
            // 1) Ia entitatea reală din DB (cu legăturile many-to-many)
            var meniuToUpdate = await _context.Meniu
                .Include(m => m.MeniuCategorii)
                    .ThenInclude(mc => mc.Categorie)
                .FirstOrDefaultAsync(m => m.ID == Meniu.ID);

            if (meniuToUpdate == null)
                return NotFound();

            // 2) Actualizează legăturile many-to-many din checkbox-uri
            UpdateMeniuCategorii(_context, selectedCategorii, meniuToUpdate);

            // 3) Actualizează câmpurile simple (inclusiv Imagine + Ingrediente)
            if (await TryUpdateModelAsync(
                meniuToUpdate,
                "Meniu",
                m => m.Denumire,
                m => m.Pret,
                m => m.DataAdaugare,
                m => m.Imagine,
                m => m.Ingrediente))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            // dacă validarea pică, refacem checkbox-urile
            PopulateAssignedCategorieData(_context, meniuToUpdate);
            return Page();
        }
    }
}
