using Microsoft.AspNetCore.Mvc;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewHomeWork.ViewComponents
{
    public class FoterViewComponent :ViewComponent
    {
        private readonly AppDbContext _db;
         public FoterViewComponent(AppDbContext db)
        {
            _db = db;     
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Bio model = _db.Bios.FirstOrDefault();
            return View(await Task.FromResult(model));
        }
    }
}
