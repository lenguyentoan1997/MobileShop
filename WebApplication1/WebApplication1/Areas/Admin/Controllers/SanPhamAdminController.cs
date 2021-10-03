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
            return View(ProductModel.Instance.DanhSachSP());
        }

        // GET: Admin/SanPhamAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/SanPhamAdmin/Create
        public ActionResult Create()
        {
            ViewBag.MaNhaSanXuat = new SelectList(NhaSanXuatModel.Instance.DanhSach(), "MaNhaSanXuat", "TenNhaSanXuat");
            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamModel.Instance.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham");
            return View();
        }

        // POST: Admin/SanPhamAdmin/Create
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(SanPham product)
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    var httpContextRequestFile = HttpContext.Request.Files[i];

                    if (HttpContext.Request.Files[i].ContentLength != 0)
                    {
                        if (i == 0)
                        {
                            string fullPathWithFileName = "~/Asset/images/" + product.MaSanPham + "_avatarV" + i + ".png";
                            httpContextRequestFile.SaveAs(Server.MapPath(fullPathWithFileName));

                            product.HinhChinh = product.MaSanPham + "_avatarV" + i + ".png";
                        }
                        else
                        {
                            string fullPathWithFileName = "~/Asset/images/" + product.MaSanPham + "thumbnail_" + i + ".png";
                            httpContextRequestFile.SaveAs(Server.MapPath(fullPathWithFileName));

                            switch (i)
                            {
                                case 1:
                                    product.Hinh1 = product.MaSanPham + "thumbnail_" + i + ".png";
                                    break;
                                case 2:
                                    product.Hinh2 = product.MaSanPham + "thumbnail_" + i + ".png";
                                    break;
                                case 3:
                                    product.Hinh3 = product.MaSanPham + "thumbnail_" + i + ".png";
                                    break;
                                case 4:
                                    product.Hinh4 = product.MaSanPham + "thumbnail_" + i + ".png";
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (i)
                        {
                            case 0:
                                product.HinhChinh = "defaultPhone.jpg";
                                break;
                            case 1:
                                product.Hinh1 = "defaultPhone.jpg";
                                break;
                            case 2:
                                product.Hinh2 = "defaultPhone.jpg";
                                break;
                            case 3:
                                product.Hinh3 = "defaultPhone.jpg";
                                break;
                            case 4:
                                product.Hinh4 = "defaultPhone.jpg";
                                break;
                        }
                    }
                }

                product.LuotView = 0;
                product.TinhTrang = "0";
                product.LikeProduct = 0;
                product.AveragePoint = 0;
                ProductModel.Instance.InsertSp(product);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private static List<string> ProductImgs = new List<string>();
        private void SelectTop5ProductImgs()
        {
            if (ProductImgs.Count > 5)
            {
                ProductImgs.RemoveRange(0, 5);
            }
        }



        private static int CountNumberImageChange { get; set; }

        // GET: Admin/SanPhamAdmin/Edit/5
        public ActionResult Edit(string id)
        {
            ViewBag.MaNhaSanXuat = new SelectList(NhaSanXuatModel.Instance.DanhSach(), "MaNhaSanXuat", "TenNhaSanXuat", ProductModel.Instance.ChiTiet(id).MaNhaSanXuat);

            ViewBag.MaLoaiSanPham = new SelectList(LoaiSanPhamModel.Instance.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham", ProductModel.Instance.ChiTiet(id).MaLoaiSanPham);

            var product = ProductModel.Instance.ChiTiet(id);

            ProductImgs.Add(product.HinhChinh);
            ProductImgs.Add(product.Hinh1);
            ProductImgs.Add(product.Hinh2);
            ProductImgs.Add(product.Hinh3);
            ProductImgs.Add(product.Hinh4);

            return View(product);
        }

        // POST: Admin/SanPhamAdmin/Edit/5
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SanPham product)
        {
            SelectTop5ProductImgs();

            List<string> productImgsList = new List<string>();
            productImgsList.Add(product.HinhChinh);
            productImgsList.Add(product.Hinh1);
            productImgsList.Add(product.Hinh2);
            productImgsList.Add(product.Hinh3);
            productImgsList.Add(product.Hinh4);

            for (int i = 0; i < productImgsList.Count; i++)
            {
                if (productImgsList[i] != null)
                {
                    CountNumberImageChange++;
                    if (i == 0)
                    {
                        string fullPathWithFileNameAvatar = "~/Asset/images/" + product.MaSanPham + "_avatarV" + CountNumberImageChange + ".png";
                        HttpContext.Request.Files[i].SaveAs(Server.MapPath(fullPathWithFileNameAvatar));
                        productImgsList[i] = product.MaSanPham + "_avatarV" + CountNumberImageChange + ".png";
                    }
                    else
                    {
                        string fullPathWithFileName = "~/Asset/images/" + product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                        HttpContext.Request.Files[i].SaveAs(Server.MapPath(fullPathWithFileName));
                    }

                    switch (i)
                    {
                        case 1:
                            productImgsList[i] = product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                            break;
                        case 2:
                            productImgsList[i] = product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                            break;
                        case 3:
                            productImgsList[i] = product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                            break;
                        case 4:
                            productImgsList[i] = product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                            break;
                    }
                }
                else
                {
                    productImgsList[i] = ProductImgs[i];
                }
            }
            product.HinhChinh = productImgsList[0];
            product.Hinh1 = productImgsList[1];
            product.Hinh2 = productImgsList[2];
            product.Hinh3 = productImgsList[3];
            product.Hinh4 = productImgsList[4];

            ProductModel.Instance.UpdateSp(product);

            return RedirectToAction("Index");
        }


        // GET: Admin/SanPhamAdmin/Delete/5
        public ActionResult Delete(string id)
        {
            return View(ProductModel.Instance.ChiTiet(id));
        }

        // POST: Admin/SanPhamAdmin/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(SanPham sp)
        {
            try
            {
                ProductModel.Instance.DeleteSp(sp);
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
