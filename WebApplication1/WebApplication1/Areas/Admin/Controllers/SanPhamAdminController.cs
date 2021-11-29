using ShopOnlineConnection;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication1.Models.BUS;


namespace WebApplication1.Areas.Admin.Controllers
{
    public class SanPhamAdminController : Controller
    {
        /*
         * Get all information products
         */
        private List<SanPham> GetAllProduct()
        {
            List<SanPham> getAllProductFromDB = new List<SanPham>();
            getAllProductFromDB.AddRange(ProductModel.Instance.DanhSachSP());

            return getAllProductFromDB;
        }

        /*
         * SelectList Producer Code, ProductTypeCode
         */
        static SelectList SelectListProducerCode;
        static SelectList SelectListProductTypeCode;

        /*
         * [Authorize(Roles = "Admin")]
         * GET: Admin/SanPhamAdmin 
         * If the parameter is not null,it will be display by queryName
         * If the parameter is null,it will be display all products
         * Search by TenSanPham or MaSanPham
         */
        public ActionResult Index(string queryName)
        {
            SelectListProducerCode = new SelectList(NhaSanXuatModel.Instance.DanhSach(), "MaNhaSanXuat", "TenNhaSanXuat");
            SelectListProductTypeCode = new SelectList(LoaiSanPhamModel.Instance.DanhSach(), "MaLoaiSanPham", "TenLoaiSanPham");

            if (queryName != null)
            {
                var resultSearch = GetAllProduct().FindAll(
                    product => product.TenSanPham.ToLower().Contains(queryName) ||
                               product.MaSanPham.ToLower().Contains(queryName));

                return View(resultSearch);
            }
            else
            {
                return View(GetAllProduct());
            }
        }

        /*
         * Display product information to create
         * GET: Admin/SanPhamAdmin/Create
         */
        public ActionResult Create()
        {
            ViewBag.MaNhaSanXuat = SelectListProducerCode;
            ViewBag.MaLoaiSanPham = SelectListProductTypeCode;

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

        /*
         * Update Product
         * List of images in the product
         */
        private static List<string> ProductImgs = new List<string>();

        //Check if in the list of images there is a count greater than 5 or if greater than 5 remove 5 images at index 0 -> 4
        private void SelectTop5ProductImgs()
        {
            if (ProductImgs.Count > 5)
            {
                ProductImgs.RemoveRange(0, 5);
            }
        }

        //Number of times the img is changed
        private static int CountNumberImageChange { get; set; }

        // GET: Admin/SanPhamAdmin/Edit/5
        // Display information products to edit
        // Get 5 images available in the product add to ProductImgs
        public ActionResult Edit(string id)
        {
            //Display SelectList MaNhaSanXuat,MaLoaiSanPham
            ViewBag.MaNhaSanXuat = SelectListProducerCode;
            ViewBag.MaLoaiSanPham = SelectListProductTypeCode;

            var getProductById = GetAllProduct().Find(product => product.MaSanPham == id);

            ProductImgs.Add(getProductById.HinhChinh);
            ProductImgs.Add(getProductById.Hinh1);
            ProductImgs.Add(getProductById.Hinh2);
            ProductImgs.Add(getProductById.Hinh3);
            ProductImgs.Add(getProductById.Hinh4);

            return View(getProductById);
        }

        // POST: Admin/SanPhamAdmin/Edit/5
        //Update product information into DB
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(SanPham product)
        {
            SelectTop5ProductImgs();

            //add the changed images to the new list
            List<string> productImgsList = new List<string>();
            productImgsList.Add(product.HinhChinh);
            productImgsList.Add(product.Hinh1);
            productImgsList.Add(product.Hinh2);
            productImgsList.Add(product.Hinh3);
            productImgsList.Add(product.Hinh4);

            //check which images have changed
            for (int i = 0; i < productImgsList.Count; i++)
            {
                //if the image changes then check at that image location
                if (productImgsList[i] != null)
                {
                    CountNumberImageChange++;

                    // i = 0 mean representative image
                    if (i == 0)
                    {
                        //Coppy representative image to file and rename by product
                        string fullPathWithFileNameAvatar = "~/Asset/images/" + product.MaSanPham + "_avatarV" + CountNumberImageChange + ".png";
                        HttpContext.Request.Files[i].SaveAs(Server.MapPath(fullPathWithFileNameAvatar));
                        productImgsList[i] = product.MaSanPham + "_avatarV" + CountNumberImageChange + ".png";
                    }
                    else
                    {
                        //Coppy secondary image avatar to file and rename by product
                        string fullPathWithFileName = "~/Asset/images/" + product.MaSanPham + "thumbnail_" + i + "V" + CountNumberImageChange + ".png";
                        HttpContext.Request.Files[i].SaveAs(Server.MapPath(fullPathWithFileName));
                    }

                    //update name image
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
                    //if the image has not changed,add the old image
                    productImgsList[i] = ProductImgs[i];
                }
            }

            //update all images
            product.HinhChinh = productImgsList[0];
            product.Hinh1 = productImgsList[1];
            product.Hinh2 = productImgsList[2];
            product.Hinh3 = productImgsList[3];
            product.Hinh4 = productImgsList[4];

            ProductModel.Instance.UpdateSp(product);
            ProductImgs.Clear();

            return RedirectToAction("Index");
        }


        /*
         * Display product information to delete
         * GET: Admin/SanPhamAdmin/Delete/5
         */
        public ActionResult Delete(string id) => View(GetAllProduct().Find(product => product.MaSanPham == id));

        // POST: Admin/SanPhamAdmin/Delete/5
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Delete(SanPham sp)
        {
            try
            {
                ProductModel.Instance.DeleteSp(sp);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
