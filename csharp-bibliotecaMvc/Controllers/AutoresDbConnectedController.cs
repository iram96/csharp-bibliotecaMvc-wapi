using csharp_bibliotecaMvc.Data;
using csharp_bibliotecaMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace csharp_bibliotecaMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresDbConnectedController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public AutoresDbConnectedController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: api/AutoresDbConnected
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autore>>> GetAutoris()
        {
            if (_context.Autoris == null)
            {
                return NotFound();
            }
            return await _context.Autoris.ToListAsync();
        }

        // GET: api/AutoresDbConnected/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Autore>> GetAutore(string id)
        {
            if (_context.Autoris == null)
            {
                return NotFound();
            }
            var autore = await _context.Autoris.FindAsync(id);

            if (autore == null)
            {
                return NotFound();
            }

            return autore;
        }

        // PUT: api/AutoresDbConnected/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutore(string id, Autore autore)
        {
            if (id != autore.Cognome)
            {
                return BadRequest();
            }

            _context.Entry(autore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AutoresDbConnected
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Autore>> PostAutore(Autore autore)
        {
            if (_context.Autoris == null)
            {
                return Problem("Entity set 'BibliotecaContext.Autoris'  is null.");
            }
            _context.Autoris.Add(autore);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AutoreExists(autore.Cognome))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAutore", new { id = autore.Cognome }, autore);
        }

        // DELETE: api/AutoresDbConnected/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutore(string id)
        {
            if (_context.Autoris == null)
            {
                return NotFound();
            }
            var autore = await _context.Autoris.FindAsync(id);
            if (autore == null)
            {
                return NotFound();
            }

            _context.Autoris.Remove(autore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AutoreExists(string id)
        {
            return (_context.Autoris?.Any(e => e.Cognome == id)).GetValueOrDefault();
        }
    }
}
