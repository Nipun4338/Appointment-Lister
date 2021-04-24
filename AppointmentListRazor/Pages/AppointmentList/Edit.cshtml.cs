using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentListRazor.Pages.AppointmentList
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Appointment Appointment { get; set; }
        public async Task OnGet(int id)
        {
            Appointment = await _db.Appointment.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var AppointmentFromDb= await _db.Appointment.FindAsync(Appointment.Id);
                AppointmentFromDb.Name = Appointment.Name;
                AppointmentFromDb.Details = Appointment.Details;
                AppointmentFromDb.Time = Appointment.Time;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
