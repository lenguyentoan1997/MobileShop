using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.Models;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class FavoriteProductController : Controller
    {
        private const string FavoriteProductSession = "FavoriteProductSession";

        // GET: FavoriteProduct
        public ActionResult Index()
        {
            var favoriteProduct = Session[CommonConstants.FavoriteProductSession];

            var listFavoriteProduct = new List<FavoriteProduct>();

            if (favoriteProduct != null)
            {
                listFavoriteProduct = (List<FavoriteProduct>)favoriteProduct;
            }
            return View(listFavoriteProduct);
        }

        [HttpPost]
        //POST: AddItem
        public JsonResult AddFavoriteProduct(string id)
        {
            var product = ProductModel.Instance.ChiTiet(id);

            var favoriteProduct = Session[FavoriteProductSession];
            if (favoriteProduct == null)
            {
                var listProduct = new List<FavoriteProduct>();

                var item = new FavoriteProduct();
                item.Product = product;
                listProduct.Add(item);

                Session[FavoriteProductSession] = listProduct;
            }
            else
            {
                var listProduct = (List<FavoriteProduct>)favoriteProduct;
                if (!listProduct.Exists(p => p.Product.MaSanPham == id))
                {
                    var item = new FavoriteProduct();
                    item.Product = product;
                    listProduct.Add(item);
                }
                Session[FavoriteProductSession] = listProduct;
            }

            return Json(new { status = true });
        }

        [HttpPost]
        public JsonResult DeleteFavoriteProduct(string productId)
        {
            var sessionFavoriteProduct = (List<FavoriteProduct>)Session[FavoriteProductSession];
            sessionFavoriteProduct.RemoveAll(p => p.Product.MaSanPham == productId);
            Session[FavoriteProductSession] = sessionFavoriteProduct;

            return Json(new { status = true });
        }

        public ActionResult CountFavoriteProduct()
        {
            var sessionFavoriteProduct = Session[FavoriteProductSession];
            var listFavoriteProduct = new List<FavoriteProduct>();

            if(sessionFavoriteProduct != null)
            {
                listFavoriteProduct = (List<FavoriteProduct>)sessionFavoriteProduct;
            }
            
            return View(listFavoriteProduct);
        }
    }
}