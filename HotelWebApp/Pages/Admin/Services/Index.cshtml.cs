using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBusiness.Models;

namespace HotelWebApp.Pages.Admin.Services
{
    public class IndexModel : PageModel
    {
        private readonly HotelBusiness.Models.HotelDbContext _context;

        public IndexModel(HotelBusiness.Models.HotelDbContext context)
        {
            _context = context;
        }

        public IList<Service> Service { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Service = await _context.Services.ToListAsync();
        }
    }
}
