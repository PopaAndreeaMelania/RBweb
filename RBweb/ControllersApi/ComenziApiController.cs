using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBweb.Data;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.ControllersApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComenziApiController : ControllerBase
    {
        private readonly RomanianBurgerWebContext _context;

        public ComenziApiController(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        // POST: api/comenziapi
        [HttpPost]
        public async Task<IActionResult> PostComanda([FromBody] ComandaDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("Cos gol.");

            var comanda = new Comanda
            {
                UserEmail = dto.UserEmail,
                DataComanda = DateTime.Now,
                Status = Comanda.StatusComanda.Noua,
                NumarComanda = "RB-" + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper()
            };

            foreach (var item in dto.Items)
            {
                var produs = await _context.Meniu.FindAsync(item.MeniuId);
                if (produs == null) continue;

                comanda.Items.Add(new ComandaItem
                {
                    MeniuID = produs.ID,
                    Cantitate = item.Cantitate,
                    Pret = produs.Pret
                });
            }

            _context.Comanda.Add(comanda);
            await _context.SaveChangesAsync();

            return Ok(new { numarComanda = comanda.NumarComanda });
        }

        // GET: api/comenziapi/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetComenziByEmail(string email)
        {
            var comenzi = await _context.Comanda
                .Include(c => c.Items)
                .ThenInclude(i => i.Meniu)
                .Where(c => c.UserEmail == email)
                .OrderByDescending(c => c.DataComanda)
                .Select(c => new
                {
                    c.ID,
                    c.NumarComanda,
                    c.DataComanda,
                    Status = c.Status.ToString(),
                    Total = c.Items.Sum(i => i.Pret * i.Cantitate)
                })
                .ToListAsync();

            return Ok(comenzi);
        }
    }

    public class ComandaDto
    {
        public string UserEmail { get; set; } = "";
        public List<ComandaItemDto> Items { get; set; } = new();
    }

    public class ComandaItemDto
    {
        public int MeniuId { get; set; }
        public int Cantitate { get; set; }
    }
}
