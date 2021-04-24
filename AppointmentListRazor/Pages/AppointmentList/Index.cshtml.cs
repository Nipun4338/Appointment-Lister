using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppointmentListRazor.Pages.AppointmentList
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Appointment> Appointments { get; set; }
        public async Task OnGet()
        {
            Appointments = await _db.Appointment.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var appointment = await _db.Appointment.FindAsync(id);
            if(appointment==null)
            {
                return NotFound();
            }
            _db.Appointment.Remove(appointment);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
