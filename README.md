using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontToUp.DAL;
using FrontToUp.Models;
using FrontToUp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FrontToUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(AppDbContext db, UserManager<AppUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                SliderText = _db.SliderTexts.FirstOrDefault(),
                SliderImages = _db.SliderImages,
                Categories = _db.Categories.Take(6),
                Valentine = _db.Valentines.FirstOrDefault(),
                ExpertText = _db.ExpertTexts.FirstOrDefault(),
                ExpertImages = _db.ExpertImages,
                BlogText = _db.BlogTexts.FirstOrDefault(),
                BlogImages = _db.BlogImages,
                Says = _db.Says,
                Instagrams = _db.Instagrams,
                Subscribe = _db.Subscribes.FirstOrDefault()
                
            };
            return View(homeVM);
        }


        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id == null) return NotFound();
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            Product product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            List<BasketVM> products;
            if (Request.Cookies["basket"] == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            BasketVM existProduct = products.FirstOrDefault(p => p.Id == id && p.UserName==User.Identity.Name);
            if (existProduct == null)
            {
                BasketVM newProduct = new BasketVM
                {
                    Id = product.Id,
                    Count = 1,
                    UserName = User.Identity.Name
                };
                products.Add(newProduct);
            }
            else
            {
                existProduct.Count++;
            }

            string basket2 = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket", basket2, new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            return Json(products.Where(p => p.UserName == User.Identity.Name).Count());
            
            //return RedirectToAction("Index");
        }

        public async Task<IActionResult> Basket()
        {
            if (User.Identity.IsAuthenticated)
            {
                string basket = Request.Cookies["basket"];
                List<BasketVM> products = new List<BasketVM>();
                List<BasketVM> userProducts = new List<BasketVM>();
                if (basket != null)
                {
                    products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                    foreach (BasketVM item in products)
                    {
                        if (item.UserName == User.Identity.Name)
                        {
                            Product dbProduct = await _db.Products.FindAsync(item.Id);
                            item.Price = dbProduct.Price;
                            item.Image = dbProduct.ImageName;
                            item.Title = dbProduct.Title;
                            userProducts.Add(item);
                        }
                    }
                    
                }
                return View(userProducts);
            }
                
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ActionName("Basket")]
        public async Task<IActionResult> BasketSale()
        {
            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
                Sale sale = new Sale
                {
                    Date = DateTime.Now,
                    AppUserId = appUser.Id,
                };
                List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                
                foreach (BasketVM item in basketProducts)
                {
                    var dbPro =await _db.Products.FindAsync(item.Id);
                    if (item.Count > dbPro.Count)
                    {
                        return Content("Get qumla oyna");
                    }
                }
                List<SaleProduct> saleProducts = new List<SaleProduct>();
                double total = 0;
                foreach (BasketVM basketProduct in basketProducts)
                {
                    if (basketProduct.UserName == User.Identity.Name)
                    {
                        Product dbProduct = await _db.Products.FindAsync(basketProduct.Id);

                        await DecreaseProductCount(dbProduct, basketProduct);

                        SaleProduct saleProduct = new SaleProduct
                        {
                            Price = dbProduct.Price,
                            Count = basketProduct.Count,
                            ProductId = basketProduct.Id,
                            SaleId = sale.Id
                        };
                        total += saleProduct.Price * saleProduct.Count;
                        saleProducts.Add(saleProduct);
                    }
                    
                }
                sale.Total = total;
                sale.SaleProducts = saleProducts;

                await _db.Sales.AddAsync(sale);
                await _db.SaveChangesAsync();
                TempData["success"] = "Alish-verishiniz ugurla yerine yetirildi";
                return RedirectToAction("Basket");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private async Task DecreaseProductCount(Product dbProduct,BasketVM basketProduct)
        {
            dbProduct.Count -= basketProduct.Count;
            await _db.SaveChangesAsync();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            string existbasket = Request.Cookies["basket"];

            List<BasketVM> products = new List<BasketVM>();

            if (existbasket != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(existbasket);
            }

            BasketVM product = products.FirstOrDefault(p => p.Id == id);
            products.Remove(product);

            string basket = JsonConvert.SerializeObject(products);
            Response.Cookies.Append("basket",basket,new CookieOptions { MaxAge=TimeSpan.FromDays(14)});

            return RedirectToAction("Basket");
        }
    }
}
