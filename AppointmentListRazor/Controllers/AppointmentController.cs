using AppointmentListRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentListRazor.Controllers
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AppointmentController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data =await _db.Appointment.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var appointmentFromDb = await _db.Appointment.FirstOrDefaultAsync(u => u.Id == id);
            if(appointmentFromDb==null)
            {
                return Json(new { success = false, message = "Error while deleting." });

            }
            _db.Appointment.Remove(appointmentFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful!" });
        }
    }
}
