using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBweb.Data;
using RomanianBurgerWeb.Data;

namespace RBweb.ControllersApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeniuApiController : ControllerBase
    {
        private readonly RomanianBurgerWebContext _context;

        public MeniuApiController(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var meniuri = await _context.Meniu
                .Select(m => new
                {
                    id = m.ID,
                    denumire = m.Denumire,
                    pret = m.Pret,
                    imagine = m.Imagine,
                    ingrediente = m.Ingrediente
                })
                .ToListAsync();

            return Ok(meniuri);
        }
    }
}
