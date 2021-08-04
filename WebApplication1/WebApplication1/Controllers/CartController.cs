using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.Models.BUS;
using System.Web.Script.Serialization;
using ShopOnlineConnection;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
       
        // GET: Cart
        //ActionResult thường được sử dụng khi bạn muốn trả về 1 view hoặc file hoặc jsondata hoặc điều hướng tới 1 url khác.
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
            var cart = Session[CartSession];
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
                Session[CartSession] = list;
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
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        //JsonResult thường được sử dụng khi bạn muốn trả về jsondata tới 1 client.
        //JsonResult là 1 loại ActionResult trong MVC. Nó giúp gửi dữ liệu theo chuẩn format JavaScript Object Notation (JSON).
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.MaSanPham == item.Product.MaSanPham);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            //jsonItem assign in sessionCart
            Session[CartSession] = sessionCart;

            //return results for cartController.js
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;

            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(string id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.MaSanPham == id);
            Session[CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();

            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreateDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            try
            {
                var id = new OrderBUS().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var orderDetailBus = new OrderDetailBUS();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.MaSanPham;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Gia;
                    orderDetail.Quantity = item.Quantity;
                    orderDetailBus.Insert(orderDetail);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return View("/Cart/Sucess");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}