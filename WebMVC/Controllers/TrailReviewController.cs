using Microsoft.AspNet.Identity;
using Models.Review.TrailReview;
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

    public class TrailReviewController : Controller
    {
        // GET: TrailReview
        public ActionResult Index(int? page)
        {
            var service = CreateTrailReviewService();
            return View(service.GetAllTrailReviewsByUser().ToPagedList(page ?? 1, 10));
        }

        // GET: TrailReview/SearchTrailReviewByName/{search}
        public ViewResult SearchTrailReviewByName(string search, int? page)
        {
            var service = CreateTrailReviewService();

            if (search == null)
            {
                return View(service.GetAllTrailReviewsByUser().ToPagedList(page ?? 1, 10));
            }

            else
            {
                return View(service.GetTrailReviewsByName(search).ToPagedList(page ?? 1, 10));
            }
        }

        //GET: TrailReview/TrailReviewDetails/{id}
        public ActionResult TrailReviewDetails(int id)
        {
            var service = CreateTrailReviewService();
            var model = service.GetTrailReviewById(id);

            return View(model);
        }

        //GET: TrailReview/TrailReviewEdit/{id}
        public ActionResult TrailReviewEdit(int id)

        {
            var service = CreateTrailReviewService();
            var detail = service.GetTrailReviewById(id);
            var model =
                new TrailReviewEdit
                {
                    ReviewId = detail.ReviewId,
                    TrailId = detail.TrailId,
                    Title = detail.Title,
                    Comment = detail.Comment,
                    Score = detail.Score,
                    VisitDate = detail.VisitDate
                };
            return View(model);
        }


        //POST: TrailReview/TrailReviewEdit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrailReviewEdit(TrailReviewEdit model)
        {
            if (ModelState.IsValid)
            {

                var service = CreateTrailReviewService();

                if (service.EditTrailReview(model))
                {
                    TempData["SaveResult"] = "Review was updated.";

                    return RedirectToAction("Index");
                }
                else
                    TempData["FailResult"] = "Review belongs to another user. Cannot update this post.";

                return RedirectToAction("Index");
            }
            else
                TempData["FailResult"] = "Review was not able to be updated.";
            return RedirectToAction("Index");

        }

        //GET: TrailReview/TrailReviewDelete/{id}
        [ActionName("TrailReviewDelete")]
        public ActionResult TrailReviewDelete(int id)
        {
            var service = CreateTrailReviewService();
            var model = service.GetTrailReviewById(id);

            return View(model);
        }


        //POST: TrailReview/TrailReviewDelete/{id}
        [HttpPost]
        [ActionName("TrailReviewDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {

            if (ModelState.IsValid)
            {

                var service = CreateTrailReviewService();

                if (service.DeleteTrailReview(id))
                {
                    TempData["SaveResult"] = "Review was deleted.";

                    return RedirectToAction("Index");
                }
                else
                    TempData["FailResult"] = "Review belongs to another user. Cannot delete this post.";

                return RedirectToAction("Index");
            }
            else
                TempData["FailResult"] = "Review was not able to be deleted.";
            return RedirectToAction("Index");
        }


        private TrailReviewService CreateTrailReviewService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TrailReviewService(userId);
            return service;
        }


    }

}