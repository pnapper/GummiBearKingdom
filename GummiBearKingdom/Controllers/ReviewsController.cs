using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GummiBearKingdom.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummiBearKingdom.Controllers
{
    public class ReviewsController : Controller
    {
        // GET: /<controller>/
        private IReviewRepository reviewRepo;  // New!

        public ReviewsController(IReviewRepository repo = null)
        {
            if (repo == null)
            {
                this.reviewRepo = new EFReviewRepository();
            }
            else
            {
                this.reviewRepo = repo;
            }
        }

        public ViewResult Index()
        {
            // Updated:
            return View(reviewRepo.Reviews.ToList());
        }

        public IActionResult Details(int id)
        {
            Review thisReview = reviewRepo.Reviews.FirstOrDefault(names => names.ReviewId == id);
            return View(thisReview);
        }

        public IActionResult Create()
        {
            //ViewBag.ReviewId = new SelectList(db.Reviews, "ReviewId", "Name", "Price", "Description"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review item)
        {
            reviewRepo.Save(item);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisReview = reviewRepo.Reviews.FirstOrDefault(names => names.ReviewId == id);
            return View(thisReview);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisReview = reviewRepo.Reviews.FirstOrDefault(names => names.ReviewId == id);
            reviewRepo.Remove(thisReview);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}