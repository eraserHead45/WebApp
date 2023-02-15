using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private AppDb app = new AppDb();

        public ActionResult UnAuthorize(string ReturnUrl)
        {
            return Redirect("/Account/Login?ReturnUrl=" + ReturnUrl);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(UserDetails userDetails)
        {   
            if(app.std2.Any(x => x.Email == userDetails.Email))
            {
                ViewBag.SignUp = "This account is already existed!!";
                return View();
            }
            else
            {
                app.std2.Add(userDetails);
                app.SaveChanges();

                Session["Index"] = userDetails.Index.ToString();
                Session["Email"] = userDetails.Email.ToString();
                return RedirectToAction("Index", "Home");
             }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login(string returnurl)
        {
            ViewBag.Url = returnurl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserDetails userDetails, string ReturnUrl)
        {
            ViewBag.Url = ReturnUrl;
            ReturnUrl = ReturnUrl ?? Url.Content("~/");

            var checkLogin = app.std2.Where(x => x.Email.Equals(userDetails.Email) && x.Password.Equals(userDetails.Password)).FirstOrDefault();

            if(checkLogin != null)
            {
                Session["Index"] = userDetails.Index.ToString();
                Session["Email"] = userDetails.Email.ToString();
                if(!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Login = "Wrong Email or Password!!!";
            }
            return View();
        }
        //USING IDENTITY FRAMEWORK

        //[HttpPost]
        //public async Task<ActionResult> Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}

        //// GET: Account
        //[HttpGet]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterDetails obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new IdentityUser { UserName = obj.Email, Email = obj.Email };
        //        var result = await userManager.CreateAsync(user, obj.Password);

        //        if (result.Succeeded)
        //        {
        //            await signInManager.SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("Index", "Home");
        //        }

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    return View(obj);
        //}

        //[HttpGet]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginDetails obj, string returnUrl)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    returnUrl = returnUrl ?? Url.Content("~/");

        //    if (ModelState.IsValid)
        //    {
        //        var result = await signInManager.PasswordSignInAsync(obj.Username, obj.Password, obj.RememberMe, false);

        //        if (result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        //            {
        //                return Redirect(returnUrl);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }

        //        ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        //    }
        //    return View(obj);
        //}
    }
}