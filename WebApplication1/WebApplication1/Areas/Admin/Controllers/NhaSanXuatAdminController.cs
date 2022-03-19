using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using static WebApplication1.Models.Database;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class NhaSanXuatAdminController : Controller
    {
        /*
         * Get all information producer from Database
         */
        [Authorize(Roles = "Admin")]
        private List<NhaSanXuatView> GetAllProducer()
        {
            List<NhaSanXuatView> getAllProducerFromDB = new List<NhaSanXuatView>();
            getAllProducerFromDB.AddRange(NhaSanXuatModel.Instance.DanhSachAdmin());

            return getAllProducerFromDB;
        }

        /*
         * [Authorize(Roles = "Admin")]
         * GET: Admin/NhaSanXuatAdmin
         * If the parameter is not null,it will be display by queryName
         * If the parameter is null,it will be display all producer
         * Search by TenNhaSanXuat,TenLoaiSanPham or MaNhaSanXuat
         */
        public ActionResult Index(string queryName)
        {
            if (queryName != null)
            {
                var resultSearch = GetAllProducer().FindAll(
                    producer => producer.TenNhaSanXuat.ToLower().Contains(queryName) ||
                                producer.TenLoaiSanPham.ToLower().Contains(queryName) ||
                                producer.MaNhaSanXuat.ToLower().Contains(queryName));

                return View(resultSearch);
            }
            else
            {
                return View(GetAllProducer());
            }
        }

        /*
         * GET: Admin/NhaSanXuatAdmin/Create
         * Display view Create
         */
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
                switch (nsx.MaLoaiSanPham)
                {
                    case "Phone":
                        nsx.MaLoaiSanPham = "LSP01";
                        break;
                    case "Laptop":
                        nsx.MaLoaiSanPham = "LSP02";
                        break;
                    case "Accessories":
                        nsx.MaLoaiSanPham = "LSP03";
                        break;
                    default:
                        break;
                }
                NhaSanXuatModel.Instance.ThemNSX(nsx);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*
         * Display information product to edit
         * GET: Admin/NhaSanXuatAdmin/Edit/5
         */
        public ActionResult Edit(string id) => View(GetAllProducer().Find(producerId => producerId.MaNhaSanXuat == id));

        // POST: Admin/NhaSanXuatAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(NhaSanXuatView nhaSanXuatView)
        {
            try
            {
                // TODO: Add update logic here
                NhaSanXuat nhaSanXuat = new NhaSanXuat();
                nhaSanXuat.TenNhaSanXuat = nhaSanXuatView.TenNhaSanXuat;
                nhaSanXuat.TinhTrang = nhaSanXuatView.TinhTrang;
                switch (nhaSanXuatView.TenLoaiSanPham)
                {
                    case "Phone":
                        nhaSanXuat.MaLoaiSanPham = "LSP01";
                        break;
                    case "Laptop":
                        nhaSanXuat.MaLoaiSanPham = "LSP02";
                        break;
                    case "Accessories":
                        nhaSanXuat.MaLoaiSanPham = "LSP03";
                        break;
                    default:
                        throw new Exception("Invalid Product Type");
                }

                NhaSanXuatModel.Instance.UpdateNSX(nhaSanXuat, nhaSanXuatView.MaNhaSanXuat);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        /*
         * Display information products to delete
         * GET: Admin/NhaSanXuatAdmin/Delete/5
         */
        public ActionResult Delete(string id) => View(GetAllProducer().Find(producerId => producerId.MaNhaSanXuat == id));

        // POST: Admin/NhaSanXuatAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(NhaSanXuat nhaSanXuat)
        {
            try
            {
                // TODO: Add delete logic here
                NhaSanXuatModel.Instance.DeleteNSX(nhaSanXuat);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
