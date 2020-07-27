using Microsoft.AspNet.Identity;
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
    public class TrailController : Controller
    {
        // GET: Trail
        public ActionResult Index(int? page)
        {
            var service = CreateTrailService();
            return View(service.GetAllTrails().ToPagedList(page ?? 1, 10));
        }

        // GET: Trail/SearchTrail/{search}
        public ViewResult SearchTrail(string search, int? page)
        {
            var service = CreateTrailService();

            if (search == null)
            {
                return View(service.GetAllTrails().ToPagedList(page ?? 1, 10));
            }

            else
            {
                return View(service.GetTrailsByName(search).ToPagedList(page ?? 1, 10));
            }
        }

        //GET: Trail/TrailCreate
        public ActionResult TrailCreate()
        {
            return View();
        }

        //POST: Trail/TrailCreate
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

        //GET: Trail/TrailDetails/{id}
        public ActionResult TrailDetails(int id)
        {
            var service = CreateTrailService();
            var model = service.GetTrailById(id);

            return View(model);
        }

        //GET: Trail/TrailEdit/{id}
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

        //POST: Trail/TrailEdit/{id}
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

        //GET: Trail/TrailDelete/{id}
        [ActionName("TrailDelete")]
        public ActionResult TrailDelete(int id)
        {
            var service = CreateTrailService();
            var model = service.GetTrailById(id);

            return View(model);
        }


        //POST: Trail/TrailDelete/{id}
        [HttpPost]
        [ActionName("TrailDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            var service = CreateTrailService();
            service.DeleteTrail(id);

            TempData["SaveResult"] = "Trail was deleted.";

            return RedirectToAction("Index");
        }

        //GET: Trail/TrailReviewCreate
        public ActionResult TrailReviewCreate()
        {
            return View();
        }

        //POST: Trail/TrailReviewCreate
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

        //GET: Trail/TrailReviewDetails/{id}
        public ActionResult TrailReviewDetails(int id)
        {
            var service = CreateTrailReviewService();
            var model = service.GetTrailReviewById(id);

            return View(model);
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