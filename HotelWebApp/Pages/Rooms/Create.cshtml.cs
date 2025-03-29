using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HotelBusiness.Models;

namespace HotelWebApp.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly HotelBusiness.Models.HotelDbContext _context;

        public CreateModel(HotelBusiness.Models.HotelDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["IdroomType"] = new SelectList(_context.RoomTypes, "IdroomType", "TypeName");
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
