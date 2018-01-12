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
        private GummiBearDbContext db = new GummiBearDbContext();

        public IActionResult Index()
        {
            ViewBag.ReviewId = new SelectList(db.Reviews, "ReviewId", "Author", "ContentBody", "Rating");
            return View();
        }

        [HttpPost]
        public IActionResult Index(Review item)
        {
            db.Reviews.Add(item);
            db.SaveChanges();
            return RedirectToAction("Details", "Product", "ProductId");
        }

        public IActionResult Details(int id)
        {
            Review thisReview = db.Reviews.FirstOrDefault(names => names.ReviewId == id);
            return View(thisReview);
        }

        public IActionResult Delete(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(names => names.ReviewId == id);
            return View(thisReview);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(names => names.ReviewId == id);
            db.Reviews.Remove(thisReview);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
