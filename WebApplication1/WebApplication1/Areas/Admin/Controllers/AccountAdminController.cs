using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class AccountAdminController : Controller
    {
        /*
         * Get all information Account Admin
         */
        private List<AspNetUser> GetAllAccountAdmin()
        {
            List<AspNetUser> getAllAccountAdminFromDB = new List<AspNetUser>();
            getAllAccountAdminFromDB.AddRange(AccountModel.Instance.ListAccocuntAdmin());

            return getAllAccountAdminFromDB;
        }

        /*
         * GET: Admin/AccountAdmin
         * If the parameter is not null,it will be display by queryName
         * If the parameter is null,it will be display all Account Admin
         * Search by FullName or Email
         */
        public ActionResult Index(string queryName)
        {
            if (queryName != null)
            {
                var resultSearch = GetAllAccountAdmin().FindAll(accountAdmin => accountAdmin.FullName == queryName || accountAdmin.Email == queryName);

                return View(resultSearch);
            }
            else
            {
                return View(GetAllAccountAdmin());
            }
        }

        /*
         * GET: Admin/AccountAdmin/Create 
         * Display Account Admin information to create
         */
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AccountAdmin/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //Here we create a Admin super user who will maintain the website
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName, CreateDate = DateTime.Now };
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

        /*
         * Display information Account Admin to edit
         * GET: Admin/AccountAdmin/Edit/5
         */
        public ActionResult Edit(string id) => View(GetAllAccountAdmin().Find(accountAdmin => accountAdmin.Id == id));

        // POST: Admin/AccountAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(AspNetUser aspNetUser, string id)
        {
            try
            {
                var accountDetails = AccountModel.Instance.AccountDetails(id);

                aspNetUser.UserName = accountDetails.UserName;
                aspNetUser.PasswordHash = accountDetails.PasswordHash;
                aspNetUser.SecurityStamp = accountDetails.SecurityStamp;
                aspNetUser.CreateDate = accountDetails.CreateDate;

                AccountModel.Instance.UpdateAdminAcocunt(aspNetUser, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*
         * Display information Account Admin to delete
         * GET: Admin/AccountAdmin/Delete/5
         */
        public ActionResult Delete(string id) => View(GetAllAccountAdmin().Find(accountAdmin => accountAdmin.Id == id));

        // POST: Admin/AccountAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(AspNetUser aspNetUser)
        {
            try
            {
                AccountModel.Instance.DeleteAdminAccount(aspNetUser);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
