using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DBFirstCRUDRazorPage.Models;

namespace DBFirstCRUDRazorPage.Pages.Owners
{
    public class CreateModel : PageModel
    {
        private readonly DBFirstCRUDRazorPage.Models.ApplicationContext _context;

        public CreateModel(DBFirstCRUDRazorPage.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Owner Owner { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Owners == null || Owner == null)
            {
                return Page();
            }

            _context.Owners.Add(Owner);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
