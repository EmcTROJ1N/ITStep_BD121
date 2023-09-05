using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFirstCRUDRazorPage.Models;

namespace DBFirstCRUDRazorPage.Pages.Owners
{
    public class DetailsModel : PageModel
    {
        private readonly DBFirstCRUDRazorPage.Models.ApplicationContext _context;

        public DetailsModel(DBFirstCRUDRazorPage.Models.ApplicationContext context)
        {
            _context = context;
        }

      public Owner Owner { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Owners == null)
            {
                return NotFound();
            }

            var owner = await _context.Owners.FirstOrDefaultAsync(m => m.Owner_Id == id);
            if (owner == null)
            {
                return NotFound();
            }
            else 
            {
                Owner = owner;
            }
            return Page();
        }
    }
}
