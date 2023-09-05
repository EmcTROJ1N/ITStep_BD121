using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirstWebApiCRUD.Data;
using DBFirstWebApiCRUD.Models;

namespace DBFirstWebApiCRUD.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly CarsContext _context;

        public OwnersController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnersTable>>> GetOwnersTables()
        {
          if (_context.OwnersTables == null)
          {
              return NotFound();
          }
            return await _context.OwnersTables.ToListAsync();
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnersTable>> GetOwnersTable(int id)
        {
          if (_context.OwnersTables == null)
          {
              return NotFound();
          }
            var ownersTable = await _context.OwnersTables.FindAsync(id);

            if (ownersTable == null)
            {
                return NotFound();
            }

            return ownersTable;
        }

        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwnersTable(int id, OwnersTable ownersTable)
        {
            if (id != ownersTable.OwnerId)
            {
                return BadRequest();
            }

            _context.Entry(ownersTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnersTableExists(id))
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

        // POST: api/Owners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OwnersTable>> PostOwnersTable(OwnersTable ownersTable)
        {
          if (_context.OwnersTables == null)
          {
              return Problem("Entity set 'CarsContext.OwnersTables'  is null.");
          }
            _context.OwnersTables.Add(ownersTable);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwnersTable", new { id = ownersTable.OwnerId }, ownersTable);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwnersTable(int id)
        {
            if (_context.OwnersTables == null)
            {
                return NotFound();
            }
            var ownersTable = await _context.OwnersTables.FindAsync(id);
            if (ownersTable == null)
            {
                return NotFound();
            }

            _context.OwnersTables.Remove(ownersTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnersTableExists(int id)
        {
            return (_context.OwnersTables?.Any(e => e.OwnerId == id)).GetValueOrDefault();
        }
    }
}
