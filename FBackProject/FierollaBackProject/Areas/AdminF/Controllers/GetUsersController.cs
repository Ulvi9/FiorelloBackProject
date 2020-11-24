using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.X509;
using PartialViewHomeWork.Dal;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.ViewModel;

namespace PartialViewHomeWork.Areas.AdminF.Controllers
{
    [Area("AdminF")]
    //[Authorize(Roles = "Admin")]
    public class GetUsersController : Controller
    {
        private readonly UserManager<AppUser> _usermanager;
        private readonly RoleManager<IdentityRole> _rolemanager;
       
        private readonly AppDbContext _db;
        public GetUsersController(UserManager<AppUser> usermanager, AppDbContext db, RoleManager<IdentityRole> rolemanager)
        {
            _rolemanager = rolemanager;
            _usermanager = usermanager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = _usermanager.Users.ToList();
            List<UserVm>usersVm= new List<UserVm>();
            foreach (AppUser user in users)
            {
                UserVm userVm = new UserVm
                {
                    Id = user.Id,
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Username = user.UserName,
                    IsActivated = user.IsActivated,
                    Role = (await _usermanager.GetRolesAsync(user))[0]
                  

                };
                usersVm.Add(userVm);
               
            }
            return View(usersVm);
        }
        public async Task<IActionResult> IsActive(string id)
        {
            AppUser appUser =await  _usermanager.FindByNameAsync(User.Identity.Name);
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
           
            if (user == null) return NotFound();
            return View(user);
           
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IsActive(string id,bool IsActivated)
        {
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
            if (user == null) return NotFound();
            user.IsActivated = IsActivated;
           await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ChangeRole(string id)
        {
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
            var Roles1 = await _rolemanager.Roles.ToListAsync();
            UserVm userVm = new UserVm
            {
                Id = user.Id,
                Fullname = user.Fullname,
                Email = user.Email,
                Username = user.UserName,
                Role = (await _usermanager.GetRolesAsync(user))[0],
               Roles = new List<string> { "Admin", "Member" },
                //Roles = Roles1
               
            };
          
            return View(userVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>ChangeRole(string id,string Role)
        {
            if (Role == null) return NotFound();
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
            if (user == null) return NotFound();
            string oldrole = (await _usermanager.GetRolesAsync(user))[0];
             await _usermanager.RemoveFromRoleAsync(user,oldrole);
            await _usermanager.AddToRoleAsync(user, Role);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> ChangePassword(string id)
        {
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(string id,string newPassword)
        {
            if (id == null) return NotFound();
            AppUser user = await _usermanager.FindByIdAsync(id);
            if (user == null) return NotFound();
           
            var token= await _usermanager.GeneratePasswordResetTokenAsync(user);
            IdentityResult identityresult = await _usermanager.ResetPasswordAsync(user, token, newPassword);
            if (!identityresult.Succeeded)
            {
                foreach (var eror in identityresult.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View();
            }
            return RedirectToAction("Index");

        }
    }
}