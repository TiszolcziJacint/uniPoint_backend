using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uniPoint_backend.Models;
using System.Threading.Tasks;
using System.Linq;

namespace uniPoint_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly uniPointContext _uniPointContext;

        public ReviewController(uniPointContext uniPointContext)
        {
            _uniPointContext = uniPointContext;
        }

        // GET: api/<ReviewController>
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var reviews = await _uniPointContext.Reviews
                                                .Include(r => r.Reviewer)
                                                .Include(r => r.Service)
                                                .ToListAsync();
            return Ok(reviews);
        }

        // GET api/<ReviewController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await _uniPointContext.Reviews
                                               .Include(r => r.Reviewer)
                                               .Include(r => r.Service)
                                               .FirstOrDefaultAsync(r => r.ReviewId == id);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        // POST api/<ReviewController>
        [HttpPost]
        public async Task<IActionResult> CreateReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _uniPointContext.Reviews.Add(review);
            await _uniPointContext.SaveChangesAsync();
            return CreatedAtAction("GetReview", new { id = review.ReviewId }, review);
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest("Id doesn't match");
            }

            var existingReview = await _uniPointContext.Reviews.FindAsync(id);
            if (existingReview == null)
            {
                return NotFound();
            }

            existingReview.Score = review.Score;
            existingReview.Description = review.Description;
            _uniPointContext.Entry(existingReview).State = EntityState.Modified;
            await _uniPointContext.SaveChangesAsync();
            return Ok(existingReview);
        }
        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _uniPointContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _uniPointContext.Reviews.Remove(review);
            await _uniPointContext.SaveChangesAsync();
            return Ok();
        }
    }
}