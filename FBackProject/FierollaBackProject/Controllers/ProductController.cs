using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ProductViewModel;

namespace PartialViewHomeWork.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        { 
            ViewBag.productcount = _db.Products.Count();
            return View(/*_db.Products.Include(p => p.Category).Take(8)*/);
        }
        public IActionResult Load(int skip)
        {
            if (skip>=_db.Products.Count())
            {
                return Content("");
            }
            IEnumerable<Product> product = _db.Products.Include(p => p.Category).Skip(skip).Take(8);
            return PartialView("_partialview", product);
            //old version
            #region
            //return Json(_db.Products.Select(p=>new ProductModel
            //{
            //    Id=p.Id,
            //    Title=p.Title,
            //    Price=p.Price,
            //    ImageName=p.ImageName,
            //    Category=p.Category
            //}).Skip(8).Take(8));
            #endregion
        }
        public IActionResult Search(string search)
        {
            IEnumerable<Product> model =_db.Products.Where(p => p.Title.Contains(search)).OrderByDescending(p=>p.Id).Take(10);
            return PartialView("_SearchProductPartial",model);
        }
    }
}