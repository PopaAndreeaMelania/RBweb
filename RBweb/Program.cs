using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RomanianBurgerWeb.Data;
using RBweb.Areas.Identity.Data;
using RBweb.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<RomanianBurgerWebContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("RomanianBurgerWebContext")
        ?? throw new InvalidOperationException("Connection string 'RomanianBurgerWebContext' not found.")
    )
);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDbContextConnection")
        ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.")
    )
);


builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


builder.Services.AddRazorPages(options =>
{
   
    options.Conventions.AuthorizeFolder("/Meniuri", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Categorii", "AdminOnly");
    options.Conventions.AuthorizeFolder("/Clienti", "AdminOnly");

    
    options.Conventions.AuthorizePage("/Comenzi/Index", "AdminOnly");
    //options.Conventions.AuthorizePage("/Comenzi/Details", "AdminOnly");

    
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Comenzi/Meniu");
});


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    
    var adminEmail = "admin@rb.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

         
        await userManager.CreateAsync(adminUser, "Admin123!");
    }

    
    if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();
