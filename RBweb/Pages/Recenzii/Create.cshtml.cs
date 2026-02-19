using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RBweb.Models;

namespace RBweb.Pages.Recenzii
{
    [Authorize]
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int ComandaID { get; set; }

        [BindProperty, Range(1, 5)]
        public int Rating { get; set; } = 5;

        [BindProperty, Required, StringLength(500)]
        public string Comentariu { get; set; } = "";

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }


        private string JsonPath => Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "recenzii.json");

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ComandaID <= 0)
            {
                ErrorMessage = "ComandaID invalid.";
                return Page();
            }

            if (!ModelState.IsValid) return Page();

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(JsonPath)!);
                if (!System.IO.File.Exists(JsonPath))
                    System.IO.File.WriteAllText(JsonPath, "[]");

                var json = System.IO.File.ReadAllText(JsonPath);
                var list = JsonSerializer.Deserialize<List<RecenzieDto>>(json) ?? new();


                if (list.Any(r => r.ComandaID == ComandaID))
                {
                    ErrorMessage = "Ai lăsat deja o recenzie pentru această comandă.";
                    return Page();
                }

                list.Add(new RecenzieDto
                {
                    ComandaID = ComandaID,
                    UserName = User.Identity?.Name ?? "Anonim",
                    Rating = Rating,
                    Comentariu = Comentariu.Trim(),
                    DataCreare = DateTime.Now
                });

                var outJson = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(JsonPath, outJson);


                return RedirectToPage("/Comenzi/Details", new { id = ComandaID });
            }
            catch
            {
                ErrorMessage = "Nu am putut salva recenzia (fișier JSON).";
                return Page();
            }
        }
    }
}