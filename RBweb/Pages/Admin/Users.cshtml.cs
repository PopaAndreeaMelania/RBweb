using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RBweb.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class UsersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UsersModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public List<UserRow> Users { get; set; } = new();

        [TempData]
        public string? Msg { get; set; }

        public class UserRow
        {
            public string Id { get; set; } = "";
            public string Email { get; set; } = "";
            public bool EmailConfirmed { get; set; }
            public List<string> Roles { get; set; } = new();
        }

        public async Task OnGetAsync()
        {
            await LoadUsersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return RedirectToPage();

            // nu permitem să-ți ștergi propriul cont (adminul logat)
            var currentId = _userManager.GetUserId(User);
            if (!string.IsNullOrWhiteSpace(currentId) && currentId == id)
            {
                Msg = "Nu poți șterge contul cu care ești logat.";
                return RedirectToPage();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                Msg = "Utilizatorul nu a fost găsit.";
                return RedirectToPage();
            }

            var result = await _userManager.DeleteAsync(user);
            Msg = result.Succeeded ? "Utilizator șters." : "Eroare la ștergere.";

            return RedirectToPage();
        }

        private async Task LoadUsersAsync()
        {
            Users.Clear();
            var list = _userManager.Users.ToList();

            foreach (var u in list)
            {
                var roles = await _userManager.GetRolesAsync(u);

                Users.Add(new UserRow
                {
                    Id = u.Id,
                    Email = u.Email ?? u.UserName ?? "",
                    EmailConfirmed = u.EmailConfirmed,
                    Roles = roles.ToList()
                });
            }

            Users = Users.OrderBy(x => x.Email).ToList();
        }
    }
}
