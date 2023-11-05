using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableCRUD.Data;
using TableCRUD.DTOs;
using TableCRUD.Models;

namespace TableCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly PubsContext _context;

        public AuthorsController(PubsContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            return await _context.Authors.ToListAsync();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(string id)
        {
          if (_context.Authors == null)
          {
              return NotFound();
          }
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(string id, Author author)
        {
            if (id != author.AuId)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDTO author)
        {
          if (_context?.Authors == null)
          {
              return Problem("Entity set 'PubsContext.Authors'  is null.");
          }
          _context.Authors.Add(new Author()
          {
                AuFname = author.AuFname,
                AuLname = author.AuLname,
                Phone = author.Phone,
                Address = author.Address,
                City = author.City,
                State = author.State,
                Zip = author.Zip,
                Contract = author.Contract
          });
          try
          {
              await _context.SaveChangesAsync();
          }
          catch (DbUpdateException)
          {
              return Conflict();
          }

          return Redirect("/authors");
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAuthorsRange(string[] ids)
        {
            foreach (string id in ids)
            {
                if (!await RemoveAuthor(id))
                    return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {
            if (await RemoveAuthor(id))
                return NoContent();
            else
                return NotFound();
        }

        private bool AuthorExists(string id)
        {
            return (_context.Authors?.Any(e => e.AuId == id)).GetValueOrDefault();
        }

        private async Task<bool> RemoveAuthor(string id)
        {
            if (_context?.Authors == null)
                return false;
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
