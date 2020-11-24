using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        public BasketController(AppDbContext db,
                                UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            Product product = await _db.Products.FindAsync(id);
            if (product.Count<1)
            {
                ModelState.AddModelError("", "Mehsuldan yoxdur");
                return BadRequest(ModelState);

            }
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
            BasketVM existone = products.FirstOrDefault(p => p.Id == id&&p.Username==User.Identity.Name);
            Product dbexistone = _db.Products.FirstOrDefault(p => p.Id == id);
            if (existone == null)
            {
                BasketVM newProduct = new BasketVM
                {
                    Id = product.Id,
                    Title = product.Title,
                    Price = product.Price,
                    ImageName = product.ImageName,
                    Count = 1,
                    Username=User.Identity.Name
                };
                products.Add(newProduct);
            }
            else
            {
                if (existone.Count<dbexistone.Count)
                {
                    existone.Count++;
                }
               
            }
            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket, new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            if (Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                return RedirectToAction(/*"Index"*/ nameof(Basket));
            }
            return Json(products.Where(p=>p.Username==User.Identity.Name).Count());

        }


        public async Task<IActionResult> Basket()
        {
            // string cookies = Request.Cookies["name"];
            //string session= HttpContext.Session.GetString("group");
            // return Content($"{session} {cookies}");
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            string basket = Request.Cookies["basket"];
            List<BasketVM> products = new List<BasketVM>();
            List<BasketVM> userProducts = new List<BasketVM>();
            if (basket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (BasketVM item in products)
                {
                    if (item.Username==User.Identity.Name)
                    {
                        Product dbProduct = await _db.Products.FindAsync(item.Id);
                        item.Price = dbProduct.Price;
                        item.ImageName = dbProduct.ImageName;
                        item.Title = dbProduct.Title;
                        item.DbCount = dbProduct.Count;
                        userProducts.Add(item);
                    }
                }
            }

            return View(userProducts);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Basket")]
        public async Task<IActionResult> SaleBasket()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            AppUser user =await _userManager.FindByNameAsync(User.Identity.Name);
            Sales sale = new Sales
            {
                Date = DateTime.Now,
                AppUserId = user.Id
            };
            List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            List<Product> dbProducts = new List<Product>();
            foreach (BasketVM item in basketProducts)
            {
                Product dbProduct = await _db.Products.FindAsync(item.Id);
                if (item.Count > dbProduct.Count)
                {
                    TempData["danger"] = $"Qaqa qalmadi sene, get sabah gelersen ((";
                    return RedirectToAction("Basket");

                }
                dbProducts.Add(dbProduct);

            }
            List<SalesProduct> salesProducts = new List<SalesProduct>();
            double total = 0;
            foreach (BasketVM pro in basketProducts)
            {
                if (pro.Username==User.Identity.Name)
                {
                    Product dbProduct = dbProducts.Find(p => p.Id == pro.Id);
                    //Product dbProduct = _db.Products.FirstOrDefault(p => p.Id == pro.Id);
                    await DecreaseCountDbProducts(dbProduct, pro);

                    SalesProduct salesProduct = new SalesProduct
                    {
                        Price = dbProduct.Price,
                        SalesId = sale.Id,
                        ProductId = pro.Id,
                        Count = pro.Count
                    };
                    total += dbProduct.Price * pro.Count;
                    salesProducts.Add(salesProduct);
                }
               
            };
            sale.Total = total;
            sale.SaleProducts = salesProducts;
            await _db.Sales.AddAsync(sale);
            await _db.SaveChangesAsync();
            TempData["success"] = "Yene gel";
            return RedirectToAction("Basket");
        }
        private async Task DecreaseCountDbProducts(Product dbProduct, BasketVM basketpro){
            
            dbProduct.Count = dbProduct.Count - basketpro.Count;
            await _db.SaveChangesAsync();

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