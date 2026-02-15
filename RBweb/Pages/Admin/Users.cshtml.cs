using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

        public class UserRow
        {
            public string Email { get; set; } = "";
            public bool EmailConfirmed { get; set; }
            public List<string> Roles { get; set; } = new();
        }

        public async Task OnGetAsync()
        {
            // Atenție: _userManager.Users e IQueryable, deci folosim ToList() ca să iterăm
            var list = _userManager.Users.ToList();

            foreach (var u in list)
            {
                var roles = await _userManager.GetRolesAsync(u);

                Users.Add(new UserRow
                {
                    Email = u.Email ?? u.UserName ?? "",
                    EmailConfirmed = u.EmailConfirmed,
                    Roles = roles.ToList()
                });
            }

            // sortare simplă
            Users = Users.OrderBy(x => x.Email).ToList();
        }
    }
}
