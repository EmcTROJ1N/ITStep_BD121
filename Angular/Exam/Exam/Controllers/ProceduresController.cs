using Exam.Data;
using Exam.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceduresController
    {
        private readonly PhoneBookDbContext _context;

        public ProceduresController(PhoneBookDbContext context)
        {
            _context = context;
        }
        
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetContactsFull()
        {
            if (_context.Contacts == null || _context.Categories == null ||
                _context.Phones == null)
            {
                return null;
            }
            
            var result = from contact in _context.Contacts
                join phone in _context.Phones on contact.PhoneId equals phone.Id
                join category in _context.Categories on contact.CategoryId equals category.Id
                select new
                {
                    contact.Id,
                    contact.FirstName,
                    contact.LastName,
                    contact.Email,
                    contact.Address,
                    contact.Birthday,
                    contact.Notes,
                    phone.PhoneNumber,
                    category.CategoryName
                };
            
            // var res = _context.Database.SqlQuery<dynamic>($"EXEC dbo.GetContactList");

            return await result.ToListAsync();

        }
    }
}