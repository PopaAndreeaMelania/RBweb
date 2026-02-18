using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Models;

namespace RBweb.Pages.Admin.Comenzi
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;

        public EditModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        // Bind-uim doar ce ne trebuie
        [BindProperty]
        public int ID { get; set; }

        [BindProperty]
        public Comanda.StatusComanda Status { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var comandaDb = await _context.Comanda.FirstOrDefaultAsync(c => c.ID == id);
            if (comandaDb == null) return NotFound();

            ID = comandaDb.ID;
            Status = comandaDb.Status;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var comandaDb = await _context.Comanda.FirstOrDefaultAsync(c => c.ID == ID);
            if (comandaDb == null) return NotFound();

            comandaDb.Status = Status;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
