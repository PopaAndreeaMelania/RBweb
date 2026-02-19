using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RBweb.Models;

namespace RBweb.Pages.Admin.Recenzii
{
    public class IndexModel : PageModel
    {
        public List<RecenzieDto> Recenzii { get; set; } = new();
        public string? ErrorMessage { get; set; }

        private string JsonPath => Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "recenzii.json");

        public void OnGet()
        {
            try
            {
                var dir = Path.GetDirectoryName(JsonPath)!;
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

                if (!System.IO.File.Exists(JsonPath))
                    System.IO.File.WriteAllText(JsonPath, "[]");

                var json = System.IO.File.ReadAllText(JsonPath);
                Recenzii = JsonSerializer.Deserialize<List<RecenzieDto>>(json) ?? new();
            }
            catch
            {
                ErrorMessage = "Nu am putut citi recenziile din recenzii.json.";
                Recenzii = new();
            }
        }
    }
}