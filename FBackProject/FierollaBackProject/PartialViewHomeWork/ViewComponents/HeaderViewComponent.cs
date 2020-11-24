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
        private readonly AppDbContext _db;
        public HeaderViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string basket = Request.Cookies["basket"];
            ViewBag.productCount = 0 ;
            int TotalPrice = 0;
            int TotalProduct = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> product = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                ViewBag.productCount = product.Count();
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
