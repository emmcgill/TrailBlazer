using Data;
using Models.Park;
using Models.Trail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ParkService
    {
        private readonly Guid _userId;

        public ParkService(Guid userId)
        {
            _userId = userId;
        }

        //CREATE PARK
        public bool CreatePark(ParkCreate model)
        {
            var entity =
                new Park()
                {
                    Name = model.Name,
                    Address = model.Address,
                    ParkSize = model.ParkSize,
                    YearEstablished = model.YearEstablished,
                    Description = model.Description
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Parks.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //GET ALL PARKS
        public IEnumerable<ParkListItem> GetAllParks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Parks
                        .Where(p => p.IsDeleted == false)
                        .Select(
                        p =>

                    new ParkListItem()
                    {
                        ParkId = p.ParkId,
                        Name = p.Name,
                        Address = p.Address,
                        ParkSize = p.ParkSize
                    }

                    );
                return query.ToArray();
            }
        }

        //GET PARKS BY NAME
        public IEnumerable<ParkListItem> GetParksByName(string search)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Parks
                        .Where(p => p.Name == search && p.IsDeleted == false)
                        .Select(
                        p =>

                    new ParkListItem()
                    {
                        ParkId = p.ParkId,
                        Name = p.Name,
                        Address = p.Address,
                        ParkSize = p.ParkSize
                    }

                    );
                return query.ToArray();
            }
        }

        //GET PARK BY ID
        public ParkDetail GetParkById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var park =
                    ctx
                        .Parks
                        .Single(p => p.ParkId == id && p.IsDeleted == false);
                return
                    new ParkDetail
                    {
                        ParkId = park.ParkId,
                        Name = park.Name,
                        Address = park.Address,
                        YearEstablished = park.YearEstablished,
                        ParkSize = park.ParkSize,
                        Description = park.Description,
                        Trails = (ICollection<TrailListItem>)GetAllParkTrailsByParkId(id),
                        AverageRating = ctx.TrailReviews.Where(p => p.Trail.ParkId == park.ParkId && p.IsDeleted == false).Average(s => (double?)s.Score) ?? 0
                    };


            }
        }

        //EDIT
        public bool EditPark(ParkEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(p => p.ParkId == model.ParkId && p.IsDeleted == false);

                entity.Name = model.Name;
                entity.Address = model.Address;
                entity.YearEstablished = model.YearEstablished;
                entity.ParkSize = model.ParkSize;
                entity.Description = model.Description;

                return ctx.SaveChanges() == 1;

            }
        }

        //DELETE
        public bool DeletePark(int parkId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Parks
                        .Single(park => park.ParkId == parkId);

                if (!entity.IsDeleted)
                {
                    entity.IsDeleted = true;
                }
                return ctx.SaveChanges() == 1;
            }
        }




        //GET ALL TRAILS BY PARK
        public IEnumerable<TrailListItem> GetAllParkTrailsByParkId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Trails
                        .Where(t => t.ParkId == id && t.IsDeleted == false)
                        .Select(
                        t =>

                    new TrailListItem()
                    {
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
    }
}
