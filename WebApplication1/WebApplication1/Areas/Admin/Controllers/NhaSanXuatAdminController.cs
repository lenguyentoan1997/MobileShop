using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class NhaSanXuatAdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        // GET: Admin/NhaSanXuatAdmin
        public ActionResult Index()
        {
            var ds = NhaSanXuatBUS.DanhSachAdmin();
            return View(ds);
        }

        // GET: Admin/NhaSanXuatAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/NhaSanXuatAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuatAdmin/Create
        [HttpPost]
        public ActionResult Create(NhaSanXuat nsx)
        {
            try
            {
                // TODO: Add insert logic here
                NhaSanXuatBUS.ThemNSX(nsx);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/NhaSanXuatAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            return View(NhaSanXuatBUS.ChiTietAdmin(id));
        }

        // POST: Admin/NhaSanXuatAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(NhaSanXuat nhaSanXuat, string maNhaSanXuat)
        {
            try
            {
                // TODO: Add update logic here
                NhaSanXuatBUS.UpdateNSX(nhaSanXuat, maNhaSanXuat);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/NhaSanXuatAdmin/Delete/5
        public ActionResult Delete(String id)
        {
            return View(NhaSanXuatBUS.ChiTietAdmin(id));
        }

        // POST: Admin/NhaSanXuatAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(NhaSanXuat nhaSanXuat)
        {
            try
            {
                // TODO: Add delete logic here
                NhaSanXuatBUS.DeleteNSX(nhaSanXuat);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
