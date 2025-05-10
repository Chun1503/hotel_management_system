using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBusiness.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelWebApp.Pages.Admin.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly HotelBusiness.Models.HotelDbContext _context;

        public IndexModel(HotelBusiness.Models.HotelDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms
                .Include(r => r.IdroomTypeNavigation).ToListAsync();
        }
    }
}
