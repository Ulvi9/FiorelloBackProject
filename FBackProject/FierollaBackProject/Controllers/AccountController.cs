using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using PartialViewHomeWork.Models;
using PartialViewHomeWork.Services;
using PartialViewHomeWork.ViewModel;

namespace PartialViewHomeWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signmanager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailInterface _emailInterface;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signmanager, IEmailInterface emailInterface, RoleManager<IdentityRole> roleManager)
        {
            _signmanager = signmanager;
            _userManager = userManager;
            _roleManager = roleManager;
            _emailInterface = emailInterface;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();
            AppUser loginuser = await _userManager.FindByEmailAsync(login.Email);
            if (loginuser==null)
            {
                ModelState.AddModelError("","Pasword ve yaxud Email yanlisdir");
                return View(login);
            }
            //if (!loginuser.IsActivated)
            //{
            //    ModelState.AddModelError("", "this user blocked");
            //    return View(login);
            //}
            var signinresult = await _signmanager.PasswordSignInAsync(loginuser, login.Password, true, true);
            if (signinresult.IsLockedOut)
            {
                ModelState.AddModelError("", "This user blocked");
                return View(login);
            }
            if (!signinresult.Succeeded)
            {
                ModelState.AddModelError("", "Pasword ve yaxud Email yanlisdir");
                return View(login);
            }
            string role = (await _userManager.GetRolesAsync(loginuser))[0];
            if (role=="Admin")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "AdminF" });
            }            
            return RedirectToAction("Index","Home");
        }
        public IActionResult Registr()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registr(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();          

            AppUser newuser = new AppUser
            {
                Fullname = register.Fullname,
                UserName = register.Username,
                Email = register.Email
            };
            IdentityResult registResult = await _userManager.CreateAsync(newuser, register.Password);
            if (!registResult.Succeeded)
            {                
                foreach (var eror in registResult.Errors)
                {
                    ModelState.AddModelError("", eror.Description);
                }
                return View(register);
            }
            ///var code = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
            //var callbackurl = Url.Action("ConfirmEmail", "Account", new { userId = newuser.Id, code = code }, protocol:/*"Https"*/ HttpContext.Request.Scheme);
            // await _emailInterface.SendEmailAsync(register.Email, "ConfirmEmail", $"Mellim service local oldugu uchun error)))<a href='{callbackurl}'>Clict to Link</a>");
            await _userManager.AddToRoleAsync(newuser, "Member");
            await _signmanager.SignInAsync(newuser, true);
            //return Content("Mailinize mesaj geldi");
            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
           await _signmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userid,string code)
        {
            if (userid == null || code == null) return NotFound();
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null) return NotFound();
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            return NotFound();
            
            
            
        }


        //public async Task CreateRole()
        //{
        //    if (!await _roleManager.RoleExistsAsync("Member"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });
        //    }
        //    if (!await _roleManager.RoleExistsAsync("Admin"))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

        //    }

        //}

    }
}