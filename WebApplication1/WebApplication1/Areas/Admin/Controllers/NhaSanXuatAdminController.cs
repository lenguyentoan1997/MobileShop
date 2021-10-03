using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using static WebApplication1.Models.Database;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class NhaSanXuatAdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        // GET: Admin/NhaSanXuatAdmin
        public ActionResult Index()
        {
            var ds = NhaSanXuatModel.Instance.DanhSachAdmin();

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

        // GET: Admin/NhaSanXuatAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            return View(NhaSanXuatModel.Instance.ChiTietAdmin(id));
        }

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

        // GET: Admin/NhaSanXuatAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            return View(NhaSanXuatModel.Instance.ChiTietAdmin(id));
        }

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
