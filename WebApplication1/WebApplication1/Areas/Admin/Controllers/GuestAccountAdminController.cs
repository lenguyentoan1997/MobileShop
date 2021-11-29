using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;


namespace WebApplication1.Areas.Admin.Controllers
{
    public class GuestAccountAdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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

        /*
         * Get all information account guest
         */
        public List<AspNetUser> GetAllAccountGuest()
        {
            List<AspNetUser> getAllAccountGuestFromDB = new List<AspNetUser>();
            getAllAccountGuestFromDB.AddRange(AccountModel.Instance.ListAccountGuest());

            return getAllAccountGuestFromDB;
        }

        /*
         * GET: Admin/GuestAccountAdmin
         * If the parameter is not null,it will be display by queryName
         * If the parameter is null,it will be display all Account Guest
         * Search by FullName,Email or PhoneNumber
         */
        public ActionResult Index(string queryName)
        {
            if (queryName != null)
            {
                var resultSearch = GetAllAccountGuest().FindAll(accountGuest => accountGuest.Email == queryName || accountGuest.FullName == queryName || accountGuest.PhoneNumber == queryName);

                return View(resultSearch);
            }
            else
            {
                return View(GetAllAccountGuest());
            }
        }

        /*
         * GET: Admin/GuestAccountAdmin/Create
         * Display information Account Admin to create
         */
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
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName,CreateDate = DateTime.Now };
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

        /*
         * Display information Account Guest to Edit
         * GET: Admin/GuestAccountAdmin/Edit/5
         */
        public ActionResult Edit(string id) => View(GetAllAccountGuest().Find(accountGuest => accountGuest.Id == id));

        // POST: Admin/GuestAccountAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(AspNetUser aspNetUser, string id)
        {
            try
            {
                var accountDetails = GetAllAccountGuest().Find(accountGuest => accountGuest.Id == id);

                aspNetUser.UserName = accountDetails.UserName;
                aspNetUser.PasswordHash = accountDetails.PasswordHash;
                aspNetUser.SecurityStamp = accountDetails.SecurityStamp;
                aspNetUser.CreateDate = accountDetails.CreateDate;

                AccountModel.Instance.UpdateGuestAccount(aspNetUser, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*
         * Change guest account password
         */
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                //sucessful password change will change to MainAdmin Index
                return RedirectToAction("Index", "");
            }

            return View(model);
        }

        /*
         * Log Out Account Guest
         * POST: /Account/LogOff
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home", new { Area = "" });

        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /*
         * Display information account guest to delete
         * GET: Admin/GuestAccountAdmin/Delete/5
         */
        public ActionResult Delete(string id) => View(GetAllAccountGuest().Find(accountGuest => accountGuest.Id == id));

        // POST: Admin/GuestAccountAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(AspNetUser aspNetUser)
        {
            try
            {
                AccountModel.Instance.DeleteGuestAccount(aspNetUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
