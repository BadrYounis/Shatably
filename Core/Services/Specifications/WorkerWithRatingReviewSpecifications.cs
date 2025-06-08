using Domain;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class WorkerWithRatingReviewSpecifications : Specifications<Worker>
    {
        public WorkerWithRatingReviewSpecifications(Guid id):base(Worker=>Worker.workerId==id)
        {
            AddInclude(Worker=>Worker.RatingReviews);
        }

        public WorkerWithRatingReviewSpecifications():base(null) 
        {
            AddInclude(Worker => Worker.RatingReviews);

        }
    }
}
