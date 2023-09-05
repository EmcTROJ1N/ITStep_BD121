using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DBFirstCRUDRazorPage.Models;

namespace DBFirstCRUDRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DBFirstCRUDRazorPage.Models.ApplicationContext _context;

        public IndexModel(DBFirstCRUDRazorPage.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Car> Car { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Cars != null)
            {
                Car = await _context.Cars.ToListAsync();
            }
        }
    }
}
