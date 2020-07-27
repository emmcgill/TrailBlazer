using Data;
using Models.Review.TrailReview;
using Models.Trail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TrailService
    {
        private readonly Guid _userId;

        public TrailService(Guid userId)
        {
            _userId = userId;
        }

        //CREATE TRAIL
        public bool CreateTrail(TrailCreate model)
        {

            var entity =
                new Trail()
                {
                    ParkId = model.ParkId,
                    Name = model.Name,
                    Length = model.Length,
                    IsTrailALoop = model.IsTrailALoop,
                    TrailDifficulty = model.TrailDifficulty,
                    Description = model.Description
                };



            using (var ctx = new ApplicationDbContext())
            {

                ctx.Trails.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //GET ALL TRAILS
        public IEnumerable<TrailListItem> GetAllTrails()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Trails
                        .Where(t => t.IsDeleted == false && t.Park.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailListItem()
                    {
                        ParkName = t.Park.Name,
                        TrailId = t.TrailId,
                        ParkId = t.ParkId,
                        Name = t.Name,
                        Length = t.Length,
                        TrailDifficulty = t.TrailDifficulty
                    }

                    );
                return query.ToArray();
            }
        }

        //GET TRAILS BY NAME
        public IEnumerable<TrailListItem> GetTrailsByName(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Trails
                        .Where(t => t.Name == search && t.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailListItem()
                    {
                        ParkName = t.Park.Name,
                        TrailId = t.TrailId,
                        ParkId = t.ParkId,
                        Name = t.Name,
                        Length = t.Length,
                        TrailDifficulty = t.TrailDifficulty
                    }

                    );
                return query.ToArray();
            }
        }

        //GET TRAIL BY ID
        public TrailDetail GetTrailById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var trail =
                    ctx
                        .Trails
                        .Single(t => t.TrailId == id && t.IsDeleted == false);
                return
                    new TrailDetail
                    {
                        TrailId = trail.TrailId,
                        ParkId = trail.ParkId,
                        Name = trail.Name,
                        Length = trail.Length,
                        Description = trail.Description,
                        IsTrailALoop = trail.IsTrailALoop,
                        TrailDifficulty = trail.TrailDifficulty,
                        AverageRating = ctx.TrailReviews.Where(t => t.TrailId == trail.TrailId && t.IsDeleted == false).Average(s => (double?)s.Score) ?? 0,
                        Reviews = (ICollection<TrailReviewListItem>)GetAllTrailReviewsByTrailId(id)

                    };
            }
        }

        //EDIT
        public bool EditTrail(TrailEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trails
                        .Single(p => p.TrailId == model.TrailId && p.IsDeleted == false);

                entity.ParkId = model.ParkId;
                entity.Name = model.Name;
                entity.Length = model.Length;
                entity.Description = model.Description;
                entity.IsTrailALoop = model.IsTrailALoop;
                entity.TrailDifficulty = model.TrailDifficulty;

                return ctx.SaveChanges() == 1;

            }
        }

        //DELETE
        public bool DeleteTrail(int trailId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Trails
                        .Single(trail => trail.TrailId == trailId);

                if (!entity.IsDeleted)
                {
                    entity.IsDeleted = true;
                }
                return ctx.SaveChanges() == 1;
            }
        }


        //GET ALL TRAIL REVIEWS BY Trail ID
        public IEnumerable<TrailReviewListItem> GetAllTrailReviewsByTrailId(int trailId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .TrailReviews
                        .Where(t => t.TrailId == trailId && t.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailReviewListItem()
                    {
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
    }
}
