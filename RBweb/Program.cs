using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Areas.Identity.Data;
using RBweb.Data;

var builder = WebApplication.CreateBuilder(args);

// =======================
// 1) DB proiect (meniuri, categorii, comenzi)
// =======================
builder.Services.AddDbContext<RomanianBurgerWebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RomanianBurgerWebContext")
        ?? throw new InvalidOperationException("Connection string 'RomanianBurgerWebContext' not found.")
    )
);

// =======================
// 2) DB Identity (AspNetUsers, AspNetRoles, etc.)
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
        ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.")
    )
);

// =======================
// 3) Identity + Roles
// =======================
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// =======================
// 4) Policy AdminOnly
// =======================
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// =======================
// 5) Razor Pages + autorizare
// =======================
builder.Services.AddRazorPages(options =>
{
    // ADMIN ONLY: CRUD
    options.Conventions.AuthorizeFolder("/Meniuri", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Categorii", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Clienti", "AdminOnly");

    // ADMIN ONLY: paginile de comenzi (scaffold)
    options.Conventions.AuthorizePage("/Comenzi/Index", "AdminOnly");
    options.Conventions.AuthorizePage("/Comenzi/Details", "AdminOnly");

    // PUBLIC/CLIENT
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Comenzi/Meniu");
});

// =======================
// 6) SESSION (coș în Session)
// =======================
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// =======================
// 7) SEED: Roluri + Admin user
// =======================
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Roluri
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Admin (SETĂM exact contul tău)
    var adminEmail = "admin@rb.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    // dacă nu există, îl creăm
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        // dacă dă eroare de complexitate, schimbăm parola cu una mai complexă
        await userManager.CreateAsync(adminUser, "Admin123!Aa");
    }

    // îl băgăm în rolul Admin
    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

// =======================
// 8) Pipeline
// =======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT: Session trebuie după Routing și înainte de MapRazorPages
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
