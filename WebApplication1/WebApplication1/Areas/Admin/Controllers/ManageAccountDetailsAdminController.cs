using ShopOnlineConnection;
using System;
using System.Web.Mvc;
using WebApplication1.Models.BUS;


namespace WebApplication1.Areas.Admin.Controllers
{
    public class ManageAccountDetailsAdminController : Controller
    {
        private AccountBUS _accountBUS = new AccountBUS();
       
        // GET: Admin/ManageAccountDetailsAdmin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/ManageAccountDetailsAdmin/Details/5
        public ActionResult Details(String id)
        {
            return View();
        }

        // GET: Admin/ManageAccountDetailsAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ManageAccountDetailsAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ManageAccountDetailsAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            return View(AccountBUS.AccountDetails(id));
        }

        // POST: Admin/ManageAccountDetailsAdmin/Edit/5
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

                _accountBUS.UpdateGuestAccount(aspNetUser, id);
                return RedirectToAction("Edit");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/ManageAccountDetailsAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: Admin/ManageAccountDetailsAdmin/Delete/5
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
