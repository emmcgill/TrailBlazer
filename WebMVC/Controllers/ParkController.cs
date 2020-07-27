using Microsoft.AspNet.Identity;
using Models.Park;
using Models.Review.TrailReview;
using Models.Trail;
using PagedList;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    [Authorize]
    public class ParkController : Controller
    {

        // GET: Park/Index
        public ViewResult Index(int? page, string sortBy)
        {
            var service = CreateParkService();
            return View(service.GetAllParks().ToPagedList(page ?? 1, 10));
        }

        // GET: Park/SearchPark/{search}
        public ViewResult SearchPark(string search, int? page)
        {
            var service = CreateParkService();

            if (search == null)
            {
                return View(service.GetAllParks().ToPagedList(page ?? 1, 10));
            }

            else
            {
                return View(service.GetParksByName(search).ToPagedList(page ?? 1, 10));
            }
        }

        //GET: Park/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Park/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParkCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateParkService();

            if (service.CreatePark(model))
            {
                TempData["SaveResult"] = "Park was created.";

                return RedirectToAction("Index");
            }

            TempData["FailResult"] = "Unable to create park.";

            return View(model);
        }

        //GET: Park/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateParkService();
            var model = service.GetParkById(id);

            return View(model);
        }

        //GET: Park/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateParkService();
            var detail = service.GetParkById(id);
            var model =
                new ParkEdit
                {
                    ParkId = detail.ParkId,
                    Name = detail.Name,
                    Address = detail.Address,
                    YearEstablished = detail.YearEstablished,
                    ParkSize = detail.ParkSize,
                    Description = detail.Description
                };
            return View(model);
        }

        //POST: Park/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParkEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ParkId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateParkService();

            if (service.EditPark(model))
            {
                TempData["SaveResult"] = "Park was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Park could not be updated.");
            return View(model);
        }


        //GET: Park/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateParkService();
            var model = service.GetParkById(id);

            return View(model);
        }


        //POST: Park/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            var service = CreateParkService();
            service.DeletePark(id);

            TempData["SaveResult"] = "Park was deleted.";

            return RedirectToAction("Index");
        }

        //GET: Park/TrailCreate
        public ActionResult TrailCreate()
        {
            return View();
        }

        //POST: Park/TrailCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrailCreate(TrailCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTrailService();

            if (service.CreateTrail(model))
            {
                TempData["SaveResult"] = "Trail was created.";

                return RedirectToAction("Index");
            }

            TempData["FailResult"] = "Unable to create trail.";

            return View(model);
        }

        //GET: Park/TrailDetails/{id}
        public ActionResult TrailDetails(int id)
        {
            var service = CreateTrailService();
            var model = service.GetTrailById(id);

            return View(model);
        }

        //GET: Park/TrailEdit/{id}
        public ActionResult TrailEdit(int id)
        {
            var service = CreateTrailService();
            var detail = service.GetTrailById(id);
            var model =
                new TrailEdit
                {
                    TrailId = detail.TrailId,
                    ParkId = detail.ParkId,
                    Name = detail.Name,
                    Length = detail.Length,
                    TrailDifficulty = detail.TrailDifficulty,
                    IsTrailALoop = detail.IsTrailALoop,
                    Description = detail.Description
                };
            return View(model);
        }

        //POST: Park/TrailEdit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrailEdit(int id, TrailEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TrailId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTrailService();

            if (service.EditTrail(model))
            {
                TempData["SaveResult"] = "Trail was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Trail could not be updated.");
            return View(model);
        }

        //GET: Park/TrailDelete/{id}
        [ActionName("TrailDelete")]
        public ActionResult TrailDelete(int id)
        {
            var service = CreateTrailService();
            var model = service.GetTrailById(id);

            return View(model);
        }


        //POST: Park/TrailDelete/{id}
        [HttpPost]
        [ActionName("TrailDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrail(int id)
        {

            var service = CreateTrailService();
            service.DeleteTrail(id);

            TempData["SaveResult"] = "Trail was deleted.";

            return RedirectToAction("Index");
        }

        //GET: Park/TrailReviewCreate
        public ActionResult TrailReviewCreate()
        {
            return View();
        }

        //POST: Park/TrailReviewCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrailReviewCreate(TrailReviewCreate model)
        {
            if (ModelState.IsValid)
            {

                var service = CreateTrailReviewService();

                if (service.ValidateTrailReviewCreate(model))
                {
                    TempData["SaveResult"] = "Review was created.";

                    return RedirectToAction("Index");
                }
                else
                    TempData["FailResult"] = "User already has a review for this trail in the system. Please edit your existing review.";

                return RedirectToAction("Index");
            }
            else
                TempData["FailResult"] = "Review was not able to be created.";
            return RedirectToAction("Index");

        }


        //GET: Park/TrailReviewDetails/{id}
        public ActionResult TrailReviewDetails(int id)
        {
            var service = CreateTrailReviewService();
            var model = service.GetTrailReviewById(id);

            return View(model);
        }

        private ParkService CreateParkService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ParkService(userId);
            return service;
        }

        private TrailService CreateTrailService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrailService(userId);
            return service;
        }

        private TrailReviewService CreateTrailReviewService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrailReviewService(userId);
            return service;
        }
    }
}