using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        private AccountBUS _accountBUS = new AccountBUS();

        // GET: Admin/AccountAdmin
        public ActionResult Index()
        {
            return View(_accountBUS.ListAdminAccocunt());
        }

        // GET: Admin/AccountAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/AccountAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AccountAdmin/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var chkUser = UserManager.Create(user, model.Password);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, "Admin");

                    return RedirectToAction("Index");
                }

            }

            return View(model);
        }

        // GET: Admin/AccountAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            return View(AccountBUS.AccountDetails(id));
        }

        // POST: Admin/AccountAdmin/Edit/5
        [HttpPost]
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

        // GET: Admin/AccountAdmin/Delete/5
        public ActionResult Delete(String id)
        {
            return View(AccountBUS.AccountDetails(id));
        }

        // POST: Admin/AccountAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(AspNetUser aspNetUser)
        {
            try
            {
                // TODO: Add delete logic here
                _accountBUS.DeleteAdminAccount(aspNetUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
