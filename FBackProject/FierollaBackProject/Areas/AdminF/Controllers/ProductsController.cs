using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartialViewHomeWork.Dal;

namespace PartialViewHomeWork.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _db;
        public ProductsController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index(int? page)
        {
            ViewBag.Page = page;
            ViewBag.Count = Math.Ceiling((decimal)_db.Products.Count() / 5);
            if (page == null)
            {

                return View(_db.Products.Include(p => p.Category).OrderByDescending(p => p.Id).Take(5).ToList());
            }

            return View(_db.Products.Include(p => p.Category).OrderByDescending(p => p.Id).Skip(((int)page - 1) * 5).Take(5).ToList());
            
          
        }
    }
}