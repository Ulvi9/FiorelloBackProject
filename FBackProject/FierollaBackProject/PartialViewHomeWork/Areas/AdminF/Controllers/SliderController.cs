using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.extensions;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.Helpers;
using Remotion.Linq.Clauses;
using Microsoft.AspNetCore.Http;

namespace PartialViewHomeWork.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IHostingEnvironment _env;
        public SliderController(AppDbContext db, IHostingEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {

            return View(_db.Sliders);
        }
        public IActionResult Create()
        {
            if (_db.Sliders.Count()>5)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #region multiple image upload;
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photos"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                ModelState.AddModelError("Photos", "Bosh qoyma");
                return View();
            }
            int canload = 5-_db.Sliders.Count();
            if (canload< slider.Photos.Length)
            {
                ModelState.AddModelError("Photos", "Yer yoxdur");
                return View();

            }
            foreach  (IFormFile photo in slider.Photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("Photos", "Sekil formati deyil");
                    return View();
                }
                if (photo.Maxlength(200))
                {
                    ModelState.AddModelError("Photos", $"{photo.FileName }Size chox boyukdur");
                }
                string filename = await photo.Saveimg(_env.WebRootPath, "img");
                Slider newslider = new Slider();
                newslider.Image = filename;
                await _db.AddAsync(newslider);
                await _db.SaveChangesAsync();
                
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region single image upload;
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Slider slider)
        //{
        //    if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
        //    {
        //        ModelState.AddModelError("Photo", "Bosh qoyma");
        //        return View();
        //    }
        //    if (!slider.Photo.IsImage())
        //    {
        //        ModelState.AddModelError("Photo", "Sekil formati deyil");
        //        return View();
        //    }
        //    if (slider.Photo.Maxlength(200))
        //    {
        //        ModelState.AddModelError("Photo", "Size chox boyukdur");
        //    }
        //    if (_db.Sliders.Count() > 5)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }

        //    string filename = await slider.Photo.Saveimg(_env.WebRootPath, "img");
        //    slider.Image = filename;
        //    await _db.AddAsync(slider);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));

        //}
        #endregion
        public async Task<IActionResult> Detail(int?id)
        {
            if (id == null) return NotFound();
            Slider slider =await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            Slider dbslider = await _db.Sliders.FindAsync(id);
            if (dbslider == null) return NotFound();
            Helper.DeleteFromRoot(_env.WebRootPath, "img", dbslider.Image);
            _db.Sliders.Remove(dbslider);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int?id)
        {
            if (id == null) return NotFound();
            Slider slider = await _db.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int ?id, Slider slider)
        {
            if (slider.Photo != null)
            {
                if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    ModelState.AddModelError("Photo", "Bosh qoyma");
                    return View();
                }
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sekil formati deyil");
                    return View();
                }
                if (slider.Photo.Maxlength(200))
                {
                    ModelState.AddModelError("Photo", "Size chox boyukdur");
                }
                Slider dbslider =await _db.Sliders.FindAsync(id);
                if (dbslider == null) return NotFound();
                Helper.DeleteFromRoot(_env.WebRootPath, "img", dbslider.Image);

                string filename = await slider.Photo.Saveimg(_env.WebRootPath, "img");
                dbslider.Image = filename;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));

        }


    }
}