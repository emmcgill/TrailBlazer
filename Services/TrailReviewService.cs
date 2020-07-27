using Data;
using Models.Review.TrailReview;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TrailReviewService
    {
        private readonly Guid _userId;

        public TrailReviewService(Guid userId)
        {
            _userId = userId;
        }

        //CREATE TRAIL REVIEW
        public bool CreateTrailReview(TrailReviewCreate model)
        {
            var entity =
                new TrailReview()
                {
                    OwnerId = _userId,
                    TrailId = model.TrailId,
                    Title = model.Title,
                    Comment = model.Comment,
                    Score = model.Score,
                    VisitDate = ValidateVisitDate(model)
                };

            using (var ctx = new ApplicationDbContext())
            {

                ctx.TrailReviews.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //VALIDATE VISIT DATE ON CREATE
        public DateTime ValidateVisitDate(TrailReviewCreate model)
        {
            var todaysDate = DateTime.Today;

            if (model.VisitDate > todaysDate)
            {
                return model.VisitDate = DateTime.Today;
            }

            else
            {
                return model.VisitDate;
            }
        }


        //VALIDATION OF TRAIL REVIEW CREATE
        public bool ValidateTrailReviewCreate(TrailReviewCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TrailReviews
                        .Where(r => r.OwnerId == _userId && r.TrailId == model.TrailId && r.IsDeleted == false);

                if (query.Count() >= 1)
                {
                    return false;
                }

                else
                {
                    return CreateTrailReview(model);
                }

            }
        }


        //GET ALL TRAIL REVIEWS BY USER
        public IEnumerable<TrailReviewListItem> GetAllTrailReviewsByUser()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TrailReviews
                        .Where(t => t.OwnerId == _userId && t.IsDeleted == false && t.Trail.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailReviewListItem()
                    {
                        TrailName = t.Trail.Name,
                        ReviewId = t.ReviewId,
                        TrailId = t.TrailId,
                        Title = t.Title,
                        Score = t.Score,
                        VisitDate = t.VisitDate
                    }

                    );
                return query.ToArray();
            }
        }


        //GET ALL TRAIL REVIEWS BY NAME
        public IEnumerable<TrailReviewListItem> GetTrailReviewsByName(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TrailReviews
                        .Where(t => t.OwnerId == _userId && t.Trail.Name == search && t.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailReviewListItem()
                    {
                        TrailName = t.Trail.Name,
                        ReviewId = t.ReviewId,
                        TrailId = t.TrailId,
                        Title = t.Title,
                        Score = t.Score,
                        VisitDate = t.VisitDate
                    }

                    );
                return query.ToArray();
            }
        }

        //GET TRAIL REVIEW BY ID
        public TrailReviewDetail GetTrailReviewById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var review =
                    ctx
                        .TrailReviews
                        .Single(r => r.ReviewId == id && r.IsDeleted == false);
                return
                    new TrailReviewDetail
                    {
                        ReviewId = review.ReviewId,
                        TrailId = review.TrailId,
                        Score = review.Score,
                        Title = review.Title,
                        Comment = review.Comment,
                        VisitDate = review.VisitDate
                    };
            }
        }

        //EDIT TRAIL REVIEW
        public bool EditTrailReview(TrailReviewEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TrailReviews
                        .Single(r => r.ReviewId == model.ReviewId && r.OwnerId == _userId);

                entity.TrailId = model.TrailId;
                entity.Title = model.Title;
                entity.Comment = model.Comment;
                entity.Score = model.Score;
                entity.VisitDate = ValidateVisitDateEdit(model);

                return ctx.SaveChanges() == 1;

            }
        }

        //VALIDATE THE VISIT DATE EDIT
        public DateTime ValidateVisitDateEdit(TrailReviewEdit model)
        {
            var todaysDate = DateTime.Today;

            if (model.VisitDate > todaysDate)
            {
                return model.VisitDate = DateTime.Today;
            }

            else
            {
                return model.VisitDate;
            }
        }


        //DELETE
        public bool DeleteTrailReview(int reviewId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .TrailReviews
                        .Single(r => r.ReviewId == reviewId && r.OwnerId == _userId);

                if (!entity.IsDeleted)
                {
                    entity.IsDeleted = true;
                }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
