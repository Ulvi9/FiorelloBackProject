using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly AppDbContext _db;
        public HeaderViewComponent(AppDbContext db, UserManager<AppUser> usermanager)
        {
            _usermanager = usermanager;
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.Fullname = "";
            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _usermanager.FindByNameAsync(User.Identity.Name);
                ViewBag.Fullname = user.Fullname;
            }
           
            string basket = Request.Cookies["basket"];
            ViewBag.productCount = 0 ;
            int TotalPrice = 0;
            int TotalProduct = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> product = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                List<BasketVM> userproduct = new List<BasketVM>();
                foreach (var item in product)
                {
                    if (item.Username==User.Identity.Name)
                    {
                        userproduct.Add(item);
                    }

                }                
                ViewBag.productCount = userproduct.Count();
                foreach (BasketVM item in product)
                {
                  TotalPrice += (int)item.Price * item.Count;
                    TotalProduct += item.Count;
                }
            }
            ViewBag.TotalPrice = TotalPrice;
            ViewBag.TotalProduct = TotalProduct;

            Bio model = _db.Bios.FirstOrDefault();
            return View(await Task.FromResult(model));            
        }
    }
}
