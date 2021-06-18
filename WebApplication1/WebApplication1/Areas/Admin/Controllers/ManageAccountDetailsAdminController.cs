using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;
using static WebApplication1.Controllers.ManageController;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Authorize]
    public class ManageAccountDetailsAdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private AccountBUS _accountBUS = new AccountBUS();

        public ManageAccountDetailsAdminController()
        {
        }

        public ManageAccountDetailsAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        // GET: /ManageAccountDetails/Index
        public ActionResult Index(AspNetUser aspNetUser,String id)
        {

            try
            {
                var accountDetails = AccountBUS.AccountDetails(id);

                aspNetUser.PasswordHash = accountDetails.PasswordHash;
                aspNetUser.SecurityStamp = accountDetails.SecurityStamp;

                _accountBUS.UpdateAdminAcocunt(aspNetUser, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(AccountBUS.AccountDetails(id));
            }
        }

        public ActionResult Edit(AspNetUser aspNetUser, String id)
        {
            try
            {
                var accountDetails = AccountBUS.AccountDetails(id);

                aspNetUser.PasswordHash = accountDetails.PasswordHash;
                aspNetUser.SecurityStamp = accountDetails.SecurityStamp;

                _accountBUS.UpdateAdminAcocunt(aspNetUser, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //public ActionResult Index(String id)
        //{

        //    return View(AccountBUS.AccountDetails(id));
        //}

        // GET: /ManageAccountDetails/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /ManageAccountDetails/ChangePassword
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
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }
    }
}
