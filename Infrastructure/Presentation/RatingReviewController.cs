using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos;
using System;
using System.Threading.Tasks;

namespace Presentation
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingReviewController(IServiceManager _serviceManager) : ControllerBase
    {
        // GET: api/RatingReview
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RatingReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _serviceManager.RatingReviewService.GetAllAsync();
            return Ok(reviews);
        }

        // GET: api/RatingReview/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RatingReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var review = await _serviceManager.RatingReviewService.GetByIdAsync(id);
            if (review == null)
                return NotFound(new { message = $"Review with ID '{id}' not found." });

            return Ok(review);
        }

        // POST: api/RatingReview
        [HttpPost]
        [ProducesResponseType(typeof(RatingReviewDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateRatingReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _serviceManager.RatingReviewService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.ReviewId }, result);
        }

        // PUT: api/RatingReview/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRatingReviewDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingReview = await _serviceManager.RatingReviewService.GetByIdAsync(id);
            if (existingReview == null)
                return NotFound(new { message = $"Review with ID '{id}' not found." });

            await _serviceManager.RatingReviewService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/RatingReview/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingReview = await _serviceManager.RatingReviewService.GetByIdAsync(id);
            if (existingReview == null)
                return NotFound(new { message = $"Review with ID '{id}' not found." });

            await _serviceManager.RatingReviewService.DeleteAsync(id);
            return NoContent();
        }
    }
}
