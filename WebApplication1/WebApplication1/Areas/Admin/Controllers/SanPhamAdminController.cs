using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;


namespace WebApplication1.Areas.Admin.Controllers
{
    public class SanPhamAdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        // GET: Admin/SanPhamAdmin
        public ActionResult Index()
        {
            return View(ShopOnlineBUS.DanhSachSP());
        }

        // GET: Admin/SanPhamAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/SanPhamAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaNhaSanXuat = new SelectList(NhaSanXuatBUS.DanhSach(), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamBUS.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham");
            return View();
        }

        // POST: Admin/SanPhamAdmin/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SanPham sp)
        {
            try
            {
                var hpf = HttpContext.Request.Files[0];
                if (hpf.ContentLength > 0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/images/" + fileName + ".png";
                    hpf.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.HinhChinh = sp.MaSanPham + ".png";
                }

                var hpf1 = HttpContext.Request.Files[1];
                if (hpf1.ContentLength > 0)
                {
                    string fileName1 = sp.MaSanPham;
                    string fullPathWithFileName1 = "~/Asset/images/" + fileName1 + "_1.png";
                    hpf1.SaveAs(Server.MapPath(fullPathWithFileName1));
                    sp.Hinh1 = sp.MaSanPham + "_1.png";
                }
                var hpf2 = HttpContext.Request.Files[2];
                if (hpf2.ContentLength > 0)
                {
                    string fileName2 = sp.MaSanPham;
                    string fullPathWithFileName2 = "~/Asset/images/" + fileName2 + "_2.png";
                    hpf2.SaveAs(Server.MapPath(fullPathWithFileName2));
                    sp.Hinh2 = sp.MaSanPham + "_2.png";
                }
                var hpf3 = HttpContext.Request.Files[3];
                if (hpf3.ContentLength > 0)
                {
                    string fileName3 = sp.MaSanPham;
                    string fullPathWithFileName3 = "~/Asset/images/" + fileName3 + "_3.png";
                    hpf3.SaveAs(Server.MapPath(fullPathWithFileName3));
                    sp.Hinh3 = sp.MaSanPham + "_3.png";
                }
                var hpf4 = HttpContext.Request.Files[4];
                if (hpf4.ContentLength > 0)
                {
                    string fileName4 = sp.MaSanPham;
                    string fullPathWithFileName4 = "~/Asset/images/" + fileName4 + "_4.png";
                    hpf4.SaveAs(Server.MapPath(fullPathWithFileName4));
                    sp.Hinh4 = sp.MaSanPham + "_4.png";
                }
                sp.TinhTrang = "0";
                sp.SoLuongDaBan = 0;
                sp.LuotView = 0;
                // TODO: Add insert logic here
                ShopOnlineBUS.InsertSp(sp);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPhamAdmin/Edit/5
        public ActionResult Edit(String id)
        {
            ViewBag.MaNhaSanXuat = new SelectList(NhaSanXuatBUS.DanhSach(), "MaNhaSanXuat", "TenNhaSanXuat", ShopOnlineBUS.ChiTiet(id).MaNhaSanXuat);
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamBUS.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham", ShopOnlineBUS.ChiTiet(id).MaLoaiSanPham);
            return View(ShopOnlineBUS.ChiTiet(id));
        }

        // POST: Admin/SanPhamAdmin/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(String id, SanPham sp)
        {
            var temp = ShopOnlineBUS.ChiTiet(id);
            try
            {
                var hpf = HttpContext.Request.Files[0];
                if (hpf.ContentLength > 0)
                {
                    string fileName = sp.MaSanPham;
                    string fullPathWithFileName = "~/Asset/images/" + fileName + "_avatar.png";
                    hpf.SaveAs(Server.MapPath(fullPathWithFileName));
                    sp.HinhChinh = sp.MaSanPham + "_avatar.png";
                }
                else
                {
                    sp.HinhChinh = temp.HinhChinh;
                }

                var hpf1 = HttpContext.Request.Files[1];
                if (hpf1.ContentLength > 0)
                {
                    string fileName1 = sp.MaSanPham;
                    string fullPathWithFileName1 = "~/Asset/images/" + fileName1 + "thumbnail_1.png";
                    hpf1.SaveAs(Server.MapPath(fullPathWithFileName1));
                    sp.Hinh1 = sp.MaSanPham + "thumbnail_1.png";
                }
                else
                {
                    sp.Hinh1 = temp.Hinh1;
                }
                var hpf2 = HttpContext.Request.Files[2];
                if (hpf2.ContentLength > 0)
                {
                    string fileName2 = sp.MaSanPham;
                    string fullPathWithFileName2 = "~/Asset/images/" + fileName2 + "thumbnail_2.png";
                    hpf2.SaveAs(Server.MapPath(fullPathWithFileName2));
                    sp.Hinh2 = sp.MaSanPham + "thumbnail_2.png";
                }
                else
                {
                    sp.Hinh2 = temp.Hinh2;
                }
                var hpf3 = HttpContext.Request.Files[3];
                if (hpf3.ContentLength > 0)
                {
                    string fileName3 = sp.MaSanPham;
                    string fullPathWithFileName3 = "~/Asset/images/" + fileName3 + "thumbnail_3.png";
                    hpf3.SaveAs(Server.MapPath(fullPathWithFileName3));
                    sp.Hinh3 = sp.MaSanPham + "thumbnail_3.png";
                }
                else
                {
                    sp.Hinh3 = temp.Hinh3;
                }
                var hpf4 = HttpContext.Request.Files[4];
                if (hpf4.ContentLength > 0)
                {
                    string fileName4 = sp.MaSanPham;
                    string fullPathWithFileName4 = "~/Asset/images/" + fileName4 + "thumbnail_4.png";
                    hpf4.SaveAs(Server.MapPath(fullPathWithFileName4));
                    sp.Hinh4 = sp.MaSanPham + "thumbnail_4.png";
                }
                else
                {
                    sp.Hinh4 = temp.Hinh4;
                }
                ShopOnlineBUS.UpdateSp(id, sp);
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/SanPhamAdmin/Delete/5
        public ActionResult Delete(String id)
        {
            return View(ShopOnlineBUS.ChiTiet(id));
        }

        // POST: Admin/SanPhamAdmin/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(String id, SanPham sp)
        {
            try
            {
                ShopOnlineBUS.DeleteSp(id);
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
