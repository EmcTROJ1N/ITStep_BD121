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
    public class CarsController : ControllerBase
    {
        private readonly CarsContext _context;

        public CarsController(CarsContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarsTable>>> GetCarsTables()
        {
          if (_context.CarsTables == null)
              return NotFound();
            return await _context.CarsTables.ToListAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarsTable>> GetCarsTable(int id)
        {
          if (_context.CarsTables == null)
          {
              return NotFound();
          }
            var carsTable = await _context.CarsTables.FindAsync(id);

            if (carsTable == null)
            {
                return NotFound();
            }

            return carsTable;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarsTable(int id, CarsTable carsTable)
        {
            if (id != carsTable.CarId)
            {
                return BadRequest();
            }

            _context.Entry(carsTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarsTableExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarsTable>> PostCarsTable(CarsTable carsTable)
        {
          if (_context.CarsTables == null)
          {
              return Problem("Entity set 'CarsContext.CarsTables'  is null.");
          }
            _context.CarsTables.Add(carsTable);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction("GetCarsTable", new { id = carsTable.CarId }, carsTable);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarsTable(int id)
        {
            if (_context.CarsTables == null)
            {
                return NotFound();
            }
            var carsTable = await _context.CarsTables.FindAsync(id);
            if (carsTable == null)
            {
                return NotFound();
            }
        
            _context.CarsTables.Remove(carsTable);
            await _context.SaveChangesAsync();
        
            return NoContent();
        }

        private bool CarsTableExists(int id)
        {
            return (_context.CarsTables?.Any(e => e.CarId == id)).GetValueOrDefault();
        }
    }
}
