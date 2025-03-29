using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HotelBusiness.Models;

namespace HotelWebApp.Pages.Bills
{
    public class BillDetailsModel : PageModel
    {
        private readonly HotelBusiness.Models.HotelDbContext _context;

        public BillDetailsModel(HotelBusiness.Models.HotelDbContext context)
        {
            _context = context;
        }

        public Bill Bill { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FirstOrDefaultAsync(m => m.Idbill == id);
            if (bill == null)
            {
                return NotFound();
            }
            else
            {
                Bill = bill;
            }
            return Page();
        }
    }
}
