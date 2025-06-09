using Domain;
using Domain.Contracts;

namespace Services.Specifications
{
    public class ServiceWithRatingReviewSpecifications : Specifications<ServicesWork>
    {
        public ServiceWithRatingReviewSpecifications(Guid id) : base(service => service.ServiceId == id)
        {
            AddInclude(service => service.RatingReviews);
        }
        public ServiceWithRatingReviewSpecifications() : base(null)
        {
            AddInclude(service => service.RatingReviews);
        }
    }
}
