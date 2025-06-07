using Domain;
using Shared;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapper
{
    public class RatingReviewMapper
    {
        public static RatingReviewDto MapToRatingReviewDTO(RatingReview ratingReview)
        {
            return new RatingReviewDto
            {
                ReviewId = ratingReview.ReviewId,
                Rating = ratingReview.Rating,
                Comment = ratingReview.Comment,
                CreatedAt = ratingReview.CreatedAt,
                ClientId = ratingReview.ClientId
            };
        }
        public static RatingReview MapToRatingReview(RatingReviewDto dto)
        {
            return new RatingReview
            {
                ReviewId = dto.ReviewId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = dto.CreatedAt,
                ClientId = dto.ClientId
            };
        }
        public static RatingReview MapToRatingReview(CreateRatingReviewDto dto)
        {
            return new RatingReview
            {
                ReviewId = Guid.NewGuid(),
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow,
                ClientId = dto.ClientId,
                WorkerId = dto.WorkerId,
                ProjectPortfolioId = dto.ProjectPortfolioId
            };
        }

    }
}
