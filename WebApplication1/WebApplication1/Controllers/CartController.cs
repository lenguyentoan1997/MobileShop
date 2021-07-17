using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.Models.BUS;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(String productId, int quantity)
        {
            var product = ShopOnlineBUS.ChiTiet(productId);
            var cart = Session[CommonConstants.CartSession];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.MaSanPham == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.Product.MaSanPham == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //new object item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstants.CartSession] = list;
            }
            else
            {
                //new object item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //assign to session
                Session[CommonConstants.CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstants.CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.MaSanPham == item.Product.MaSanPham);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstants.CartSession] = sessionCart;
            //return results for cartController.js
            return Json(new
            {
                status = true
            });
        }
    }
}