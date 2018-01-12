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
    public class ProductsController : Controller
    {
        // GET: /<controller>/
        private IProductRepository productRepo;  // New!

        public ProductsController(IProductRepository repo = null)
        {
            if (repo == null)
            {
                this.productRepo = new EFProductRepository();
            }
            else
            {
                this.productRepo = repo;
            }
        }

        public ViewResult Index()
        {
            // Updated:
            return View(productRepo.Products.ToList());
        }

        public IActionResult Details(int id)
        {
            Product thisProduct = productRepo.Products.FirstOrDefault(names => names.ProductId == id);
            return View(thisProduct);
        }

        public IActionResult Create()
        {
            //ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name", "Price", "Description"); 
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product item)
        {
            productRepo.Save(item);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(names => names.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost]
        public IActionResult Edit(Product item)
        {
            productRepo.Edit(item);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(names => names.ProductId == id);
            return View(thisProduct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisProduct = productRepo.Products.FirstOrDefault(names => names.ProductId == id);
            productRepo.Remove(thisProduct);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
