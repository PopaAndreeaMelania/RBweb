using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RomanianBurgerWeb.Data;
using RBweb.Helpers;
using RBweb.Models;
using RBweb.Models.ViewModels;

namespace RBweb.Pages.Comenzi
{
    [Authorize]
    public class PlaceOrderModel : PageModel
    {
        private readonly RomanianBurgerWebContext _context;
        private const string CART_KEY = "CART";

        public PlaceOrderModel(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        public List<CartItemVM> Cart { get; set; } = new();
        public decimal Total => Cart.Sum(x => x.Pret * x.Cantitate);

        [BindProperty]
        public string? Mentiuni { get; set; }

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObject<List<CartItemVM>>(CART_KEY) ?? new();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(email)) return Challenge();

            var cart = HttpContext.Session.GetObject<List<CartItemVM>>(CART_KEY) ?? new();
            if (!cart.Any()) return RedirectToPage("/Comenzi/Cos");

            var comanda = new Comanda
            {
                UserEmail = email,
                DataComanda = DateTime.Now,
                Mentiuni = Mentiuni
            };

            comanda.NumarComanda = "RB-" + Guid.NewGuid().ToString("N")[..6].ToUpper();

            foreach (var ci in cart)
            {
                comanda.Items.Add(new ComandaItem
                {
                    MeniuID = ci.MeniuID,
                    Cantitate = ci.Cantitate,
                    Pret = ci.Pret
                });
            }

            _context.Comanda.Add(comanda);
            await _context.SaveChangesAsync();


            HttpContext.Session.Remove(CART_KEY);

            return RedirectToPage("/Comenzi/Confirmare");
        }
    }
}
