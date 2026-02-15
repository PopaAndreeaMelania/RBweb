using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RBweb.Helpers;
using RBweb.Models.ViewModels;

namespace RBweb.Pages.Comenzi
{
    [Authorize]
    public class CosModel : PageModel
    {
        private const string CART_KEY = "CART";

        public List<CartItemVM> Cart { get; set; } = new();
        public decimal Total => Cart.Sum(x => x.Pret * x.Cantitate);

        public void OnGet()
        {
            Cart = HttpContext.Session.GetObject<List<CartItemVM>>(CART_KEY) ?? new();
        }

        public IActionResult OnPostUpdate(Dictionary<int, int> qty)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemVM>>(CART_KEY) ?? new();

            foreach (var item in cart)
            {
                if (qty.TryGetValue(item.MeniuID, out var newQty))
                    item.Cantitate = Math.Max(1, newQty);
            }

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int meniuId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemVM>>(CART_KEY) ?? new();
            cart.RemoveAll(x => x.MeniuID == meniuId);

            HttpContext.Session.SetObject(CART_KEY, cart);
            return RedirectToPage();
        }
    }
}
