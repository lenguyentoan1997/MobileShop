using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    public class AccountManagementAdminController : Controller
    {
        // GET: Admin/AccountManagementAdmin
        public ActionResult Index()
        {
            var db = AccountBUS.ListAccount();
            return View(db);
        }

        // GET: Admin/AccountManagementAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/AccountManagementAdmin/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Admin/AccountManagementAdmin/Create
        [HttpPost]

        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                //waitting....
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/AccountManagementAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/AccountManagementAdmin/Edit/5
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

        // GET: Admin/AccountManagementAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/AccountManagementAdmin/Delete/5
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
