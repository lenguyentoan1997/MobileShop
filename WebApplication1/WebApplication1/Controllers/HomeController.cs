using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Common;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //
        [ChildActionOnly]
        public PartialViewResult _Header()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return PartialView(list);
        }
    }
}