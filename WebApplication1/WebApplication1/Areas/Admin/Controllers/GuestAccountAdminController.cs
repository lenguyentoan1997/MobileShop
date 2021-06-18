using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Models;
using WebApplication1.Models.BUS;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class GuestAccountAdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private AccountBUS _accountBUS = new AccountBUS();

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/GuestAccountAdmin
        public ActionResult Index()
        {
            return View(_accountBUS.ListGuestAccount());
        }

        // GET: Admin/GuestAccountAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/GuestAccountAdmin/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/GuestAccountAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        // GET: Admin/GuestAccountAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            return View(AccountBUS.AccountDetails(id));
        }


        // POST: Admin/GuestAccountAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(AspNetUser aspNetUser, String id)
        {
            try
            {
                // TODO: Add update logic here
                var accountDetails = AccountBUS.AccountDetails(id);

                aspNetUser.UserName = accountDetails.UserName;
                aspNetUser.PasswordHash = accountDetails.PasswordHash;
                aspNetUser.SecurityStamp = accountDetails.SecurityStamp;
                //Waitting..................
                _accountBUS.UpdateGuestAccount(aspNetUser, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/GuestAccountAdmin/Delete/5
        public ActionResult Delete(String id)
        {
            return View(AccountBUS.AccountDetails(id));
        }

        // POST: Admin/GuestAccountAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(AspNetUser aspNetUser)
        {
            try
            {
                // TODO: Add delete logic here
                _accountBUS.DeleteGuestAccount(aspNetUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
