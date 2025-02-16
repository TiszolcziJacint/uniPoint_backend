using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uniPoint_backend.Models;
using System.Threading.Tasks;
using System.Linq;

namespace uniPoint_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly uniPointContext _uniPointContext;

        public AppointmentController(uniPointContext uniPointContext)
        {
            _uniPointContext = uniPointContext;
        }

        // GET: api/<AppointmentController>
        [HttpGet]
        public async Task<IActionResult> GetAppointments()
        {
            var appointments = await _uniPointContext.Appointments
                                                     .Include(a => a.Booker)
                                                     .Include(a => a.Service)
                                                     .ToListAsync();
            return Ok(appointments);
        }

        // GET api/<AppointmentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            var appointment = await _uniPointContext.Appointments
                                                    .Include(a => a.Booker)
                                                    .Include(a => a.Service)
                                                    .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        // POST api/<AppointmentController>
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _uniPointContext.Appointments.Add(appointment);
            await _uniPointContext.SaveChangesAsync();
            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        // PUT api/<AppointmentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest("Id doesn't match");
            }

            var existingAppointment = await _uniPointContext.Appointments.FindAsync(id);
            if (existingAppointment == null)
            {
                return NotFound();
            }

            existingAppointment.ScheduledAt = appointment.ScheduledAt;
            existingAppointment.Status = appointment.Status;
            _uniPointContext.Entry(existingAppointment).State = EntityState.Modified;
            await _uniPointContext.SaveChangesAsync();
            return Ok(existingAppointment);
        }

        // DELETE api/<AppointmentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _uniPointContext.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _uniPointContext.Appointments.Remove(appointment);
            await _uniPointContext.SaveChangesAsync();
            return Ok();
        }
    }
}