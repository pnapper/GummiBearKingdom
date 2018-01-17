using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GummiBearKingdom.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GummiBearKingdom.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository productRepo = new EFProductRepository();

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> topRatedProducts = productRepo.Products
                                                        .Include(x => x.Reviews)
                                                        .OrderByDescending(x => x.ProductId).Take(3).ToList();
            return View(topRatedProducts);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
