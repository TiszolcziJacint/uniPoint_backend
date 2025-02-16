using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uniPoint_backend.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace uniPoint_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly uniPointContext _uniPointContext;

        public ServiceController(uniPointContext uniPointContext)
        {
            _uniPointContext = uniPointContext;
        }

        // GET: api/<ServiceController>
        [HttpGet]
        public async Task<IActionResult> GetServices()
        {
            var services = await _uniPointContext.Services.Include(s => s.Provider).ToListAsync();
            return Ok(services);
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetService(int id)
        {
            var service = await _uniPointContext.Services.Include(s => s.Provider)
                                                          .FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<IActionResult> CreateService(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _uniPointContext.Services.Add(service);
            await _uniPointContext.SaveChangesAsync();
            return CreatedAtAction("GetService", new { id = service.ServiceId }, service);
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, Service service)
        {
            if (id != service.ServiceId)
            {
                return BadRequest("Id doesn't match");
            }

            var existingService = await _uniPointContext.Services.FindAsync(id);
            if (existingService == null)
            {
                return NotFound();
            }

            existingService.ServiceName = service.ServiceName;
            existingService.Price = service.Price;
            existingService.Description = service.Description;
            existingService.Address = service.Address;
            existingService.Duration = service.Duration;

            _uniPointContext.Entry(existingService).State = EntityState.Modified;
            await _uniPointContext.SaveChangesAsync();
            return Ok(existingService);
        }
        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var service = await _uniPointContext.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _uniPointContext.Services.Remove(service);
            await _uniPointContext.SaveChangesAsync();
            return Ok();
        }
    }
}
