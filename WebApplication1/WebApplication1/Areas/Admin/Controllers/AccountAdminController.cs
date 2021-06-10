using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            // TODO: Add insert logic here
            if (ModelState.IsValid)
            {
                ApplicationDbContext context = new ApplicationDbContext();

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                //in Startup iam creating first Admin Role and creating a default Admin User
                if (!roleManager.RoleExists("Admin"))
                {
                    //first we create Admin role
                    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);

                    //Here we create a Admin super user who will maintain the website
                    var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
                    var chkUser = await UserManager.CreateAsync(user, model.Password);

                    //Add default User to Role Admin
                    if (chkUser.Succeeded)
                    {
                        var result = UserManager.AddToRole(user.Id, "Admin");

                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);

            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber };
            //    var result = await UserManager.CreateAsync(user, model.Password);
            //    if (result.Succeeded)
            //    {
            //        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

            //        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            //        // Send an email with this link
            //        // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            //        // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            //        // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


            //    }
            //}

            //ApplicationDbContext context = new ApplicationDbContext();

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ////in Startup iam creating first Admin Role and creating a default Admin User
            //if (!roleManager.RoleExists("Admin"))
            //{
            //    //first we create Admin rool
            //    var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            //    role.Name = "Admin";
            //    roleManager.Create(role);

            //    //Here we create a Admin super user who will maintain the website
            //    var user = new ApplicationUser();
            //    user.UserName = "admin@gmail.com";
            //    user.Email = "admin@gmail.com";

            //    string userPWD = "A@Z1112";

            //    var chkUser = UserManager.Create(user, userPWD);

            //    //Add default User to Role Admin
            //    if (chkUser.Succeeded)
            //    {
            //        var result = UserManager.AddToRole(user.Id, "Admin");
            //    }
            //}
        }

        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Admin/AccountAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/AccountAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/AccountAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/AccountAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
