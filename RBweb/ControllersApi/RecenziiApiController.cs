using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RBweb.Models;
using RomanianBurgerWeb.Data;

namespace RBweb.ControllersApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecenziiApiController : ControllerBase
    {
        private readonly RomanianBurgerWebContext _context;

        public RecenziiApiController(RomanianBurgerWebContext context)
        {
            _context = context;
        }

        // POST: api/recenziiapi
        [HttpPost]
        public async Task<IActionResult> PostRecenzie([FromBody] RecenzieDto dto)
        {
            if (dto.Rating < 1 || dto.Rating > 5)
                return BadRequest("Rating invalid.");

            var recenzie = new Recenzie
            {
                ComandaID = dto.ComandaID,
                Rating = dto.Rating,
                Comentariu = dto.Comentariu,
                DataCreare = DateTime.Now
            };

            _context.Recenzii.Add(recenzie);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}