using System;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using ShopOnlineConnection;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class LoaiSanPhamAdminController : Controller
    {
        //[Authorize(Roles ="Admin")]
        // GET: Admin/LoaiSanPhamAdmin
        public ActionResult Index()
        {
            var db = LoaiSanPhamBUS.DanhSachAdmin();
            return View(db);
        }

        // GET: Admin/LoaiSanPhamAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/LoaiSanPhamAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiSanPhamAdmin/Create
        [HttpPost]
        public ActionResult Create(LoaiSanPham lsp)
        {
            try
            {
                // TODO: Add insert logic here
                LoaiSanPhamBUS.AddLoaiSanPham(lsp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/LoaiSanPhamAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            return View(LoaiSanPhamBUS.ChiTietLSP(id));
        }

        // POST: Admin/LoaiSanPhamAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(String maLoaiSanPham, String tenLoaiSanPham, String tinhTrang)
        {
            try
            {
                // TODO: Add update logic here
                LoaiSanPhamBUS.UpdateLSP(maLoaiSanPham, tenLoaiSanPham, tinhTrang);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/LoaiSanPhamAdmin/Delete/5
        public ActionResult Delete(String id)
        {
            return View(LoaiSanPhamBUS.ChiTietLSP(id));
        }

        // POST: Admin/LoaiSanPhamAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(String maLoaiSanPham, String id)
        {
            try
            {
                // TODO: Add delete logic here
                LoaiSanPhamBUS.DeleteLSP(maLoaiSanPham);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
