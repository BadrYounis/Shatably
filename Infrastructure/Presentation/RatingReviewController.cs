using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;

namespace Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingReviewController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/RatingReview
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _serviceManager.RatingReviewService.GetAllAsync();
            return Ok(reviews);
        }

        // GET: api/RatingReview/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await _serviceManager.RatingReviewService.GetByIdAsync(id);
            return Ok(review);
        }

        // POST: api/RatingReview
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRatingReviewDto dto)
        {
            var result = await _serviceManager.RatingReviewService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.ReviewId }, result);
        }

        // PUT: api/RatingReview/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRatingReviewDto dto)
        {
            await _serviceManager.RatingReviewService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/RatingReview/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _serviceManager.RatingReviewService.DeleteAsync(id);
            return NoContent();
        }
    }
}
