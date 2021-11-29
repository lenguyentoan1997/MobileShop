using System.Web.Mvc;
using WebApplication1.Models.BUS;
using ShopOnlineConnection;
using System.Collections.Generic;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class LoaiSanPhamAdminController : Controller
    {
        /*
         * Get all information product type from database
         */
        private List<LoaiSanPham> GetAllProductType()
        {
            List<LoaiSanPham> getAllProductTypeFromDB = new List<LoaiSanPham>();
            getAllProductTypeFromDB.AddRange(LoaiSanPhamModel.Instance.DanhSachAdmin());

            return getAllProductTypeFromDB;
        }

        /*
         * [Authorize(Roles ="Admin")]
         * GET: Admin/LoaiSanPhamAdmin
         * If the parameter is not null,it will be display by queryName
         * If the parameter is null,it will be display all product type
         * Search by TenLoaiSanPham or MaLoaiSanPham
         */
        public ActionResult Index(string queryName)
        {
            if (queryName != null)
            {
                var resultSearch = GetAllProductType().FindAll(
                    producType => producType.TenLoaiSanPham.ToLower().Contains(queryName) ||
                                  producType.MaLoaiSanPham.ToLower().Contains(queryName));

                return View(resultSearch);
            }
            else
            {
                return View(GetAllProductType());
            }
        }

        /*
         * Display view create
         * GET: Admin/LoaiSanPhamAdmin/Create
         */
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
                LoaiSanPhamModel.Instance.AddLoaiSanPham(lsp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*
         * Display information product Type to edit
         * GET: Admin/LoaiSanPhamAdmin/Edit/5
         */
        public ActionResult Edit(string id) => View(GetAllProductType().Find(productType => productType.MaLoaiSanPham == id));

        // POST: Admin/LoaiSanPhamAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(LoaiSanPham loaiSanPham, string maLoaiSanPham)
        {
            try
            {
                // TODO: Add update logic here
                LoaiSanPhamModel.Instance.UpdateLSP(loaiSanPham, maLoaiSanPham);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        /*
         * Display information product type to delete
         * GET: Admin/LoaiSanPhamAdmin/Delete/5
         */
        public ActionResult Delete(string id) => View(GetAllProductType().Find(productType => productType.MaLoaiSanPham == id));

        // POST: Admin/LoaiSanPhamAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(LoaiSanPham loaiSanPham)
        {
            try
            {
                // TODO: Add delete logic here
                LoaiSanPhamModel.Instance.DeleteLSP(loaiSanPham);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
