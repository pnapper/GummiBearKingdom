﻿using System;
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
        private GummiBearDbContext db = new GummiBearDbContext();
        public IActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public IActionResult Details(int id)
        {
            Product thisProduct = db.Products.FirstOrDefault(names => names.ProductId == id);
            return View(thisProduct);
        }
    }
}