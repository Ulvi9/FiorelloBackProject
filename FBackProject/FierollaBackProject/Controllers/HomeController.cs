using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ViewModel;

namespace PartialViewHomeWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //HttpContext.Session.SetString("group","p116");
            //Response.Cookies.Append("name", "Ulvi", new CookieOptions { MaxAge = TimeSpan.FromMinutes(20) });
            HomeView homeview = new HomeView
            {
               
                Categories = _db.Categories,
                sliders=_db.Sliders
                //Products = _db.Products.Include(p => p.Category).Take(8)
            };
           
            return View(homeview);
        }
    }
}