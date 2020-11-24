using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ViewModel;

namespace PartialViewHomeWork.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _db;
        public BasketController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            List<BasketVM> products;
            string exist = Request.Cookies["basket"];
            if (exist == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(exist);
            }
            BasketVM existone = products.FirstOrDefault(p => p.Id == id);
            if (existone == null)
            {
                BasketVM newProduct = new BasketVM
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    Count = 1
                };
                products.Add(newProduct);
            }
            else
            {
                existone.Count++;
            }
            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                return RedirectToAction(/*"Index"*/ nameof(Basket));
            }
            return Json(products.Count());

        }


        public async Task<IActionResult> Basket()
        {
            // string cookies = Request.Cookies["name"];
            //string session= HttpContext.Session.GetString("group");
            // return Content($"{session} {cookies}");
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = new List<BasketVM>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (BasketVM item in products)
                {

                    Product dbProduct = await _db.Products.FindAsync(item.Id);
                    item.Price = dbProduct.Price;
                    item.ImageName = dbProduct.ImageName;
                    item.Title = dbProduct.Title;

                }
            }


            return View(products);
        }
        public IActionResult RemoveFromBasket(int id)
        {
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            products.Remove(products.Find(p => p.Id == id));

            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            return RedirectToAction(/*"Index"*/ nameof(Basket));


        }
        public IActionResult Decrease(int id)
        {
            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM product = products.Where(p => p.Id == id).FirstOrDefault();
            if (product.Count > 1)
            {
                --product.Count;
            }
            else
            {
                products.Remove(product);
            }
            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            return RedirectToAction(/*"Index"*/ nameof(Basket));

        }
    }
}