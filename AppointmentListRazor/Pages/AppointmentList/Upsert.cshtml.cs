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
    public class UpsertModel : PageModel
    {
        private ApplicationDbContext _db;
        public UpsertModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public Appointment Appointment { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            Appointment = new Appointment();
            if(id==null)
            {
                //create
                return Page();
            }

            //update
            Appointment = await _db.Appointment.FirstOrDefaultAsync(u => u.Id == id);
            if(Appointment==null)
            {
                return NotFound();
            }
            return Page();
            }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if(Appointment.Id==0)
                {
                    _db.Appointment.Add(Appointment);
                }
                else
                {
                    _db.Appointment.Update(Appointment);
                }
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}
