using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Exam.Data;
using Exam.Models;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;

        public ContactsController(PhoneBookDbContext context)
        {
            _context = context;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContacts()
        {
          if (_context.Contacts == null)
          {
              return NotFound();
          }
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Contacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
          if (_context.Contacts == null)
          {
              return NotFound();
          }
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Contacts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, ContactDTO contact)
        {
            Contact[] tmp = (from c in _context.Contacts
                where c.Id == id
                select c).ToArray();
            if (tmp.Length != 1)
                return BadRequest();
            Contact current = tmp.First();
            current.FirstName = contact.FirstName;
            current.LastName = contact.LastName;
            current.Email = contact.Email;
            current.Address = contact.Address;
            current.Birthday = contact.Birthday;
            current.Notes = contact.Notes;
            current.PhoneId = contact.PhoneId;
            current.CategoryId = contact.CategoryId;
            
            _context.Entry(current).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(ContactDTO contact)
        {
          if (_context.Contacts == null)
          {
              return Problem("Entity set 'PhoneBookDbContext'  is null.");
          }
            Contact contactResult = new Contact()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Address = contact.Address,
                Birthday = contact.Birthday,
                Notes = contact.Notes,
                PhoneId = contact.PhoneId,
                CategoryId = contact.CategoryId
            };
            _context.Contacts.Add(contactResult);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contactResult.Id }, contactResult);
        }

        // DELETE: api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            if (_context.Contacts == null)
            {
                return NotFound();
            }
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
