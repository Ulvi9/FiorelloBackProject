using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;

namespace PartialViewHomeWork.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Categories);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FindAsync(id);
            if (category ==null) return NotFound();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(/*string Name,string Description*/Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool isValid = _db.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isValid)
            {
                ModelState.AddModelError("Name", "Bu ad categoriya movcuddur");
                return View();
            }
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");


        }

        public async Task< IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }            
            Category dbcategory = await _db.Categories.FindAsync(category.Id);
            if(dbcategory == null) return NotFound();
            Category nameCategory = _db.Categories.FirstOrDefault(p => p.Name.ToLower() == category.Name.ToLower());
           
            if (nameCategory!=null)
            {
                if (nameCategory.Name != dbcategory.Name)
                {
                    ModelState.AddModelError("Name", "Bu ad categoriya movcuddur");
                    return View();
                }
            }            
            dbcategory.Name = category.Name;
            dbcategory.Description = category.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            Category category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}