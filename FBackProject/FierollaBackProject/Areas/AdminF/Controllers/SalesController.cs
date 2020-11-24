using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ViewModel;

namespace PartialViewHomeWork.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class SalesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public SalesController(UserManager<AppUser> userManager, AppDbContext db)
        {
            _userManager = userManager;
            _db = db;

        }
        public async Task<IActionResult> Index()
        {
            List<Sales>sales= _db.Sales.ToList();
            List<SalesVM> saleVm = new List<SalesVM>();
            
            foreach (Sales item in sales)
            {
                AppUser user = await _userManager.FindByIdAsync(item.AppUserId);
                SalesVM newsalevm = new SalesVM
                {
                    Id = item.Id,
                    Total = item.Total,
                    Date = item.Date,
                    Email = user.Email,
                    Fullname = user.Fullname,
                    SaleProducts = _db.SalesProducts.Where(sp => sp.SalesId == item.Id).Include(sp => sp.Product).ToList()
                };
                saleVm.Add(newsalevm);
            }
            return View(saleVm);
          
        }
    }
}