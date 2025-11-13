using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using WebApplication.Data;

namespace WebApplication.Controllers;

public class PlayersController
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Players>>> GetPlayers()
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Players>> GetPlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // POST: api/players
        [HttpPost]
        public async Task<ActionResult<Players>> CreatePlayer(Players player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
        }

        // PUT: api/players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, Players player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}