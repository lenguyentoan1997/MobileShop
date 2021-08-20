using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using PagedList;
using ShopOnlineConnection;

namespace WebApplication1.Controllers
{
    public class NhaSanXuatController : Controller
    {
        public static string GetProducer { get; set; }
        // GET: NhaSanXuat
        public ActionResult Index(string id, int page = 1, int pagesize = 4)
        {
            var db = NhaSanXuatBUS.ChiTiet(id).ToPagedList(page, pagesize);
            switch (id)
            {
                case "NSX01":
                    GetProducer = "Iphone"; break;
                case "NSX02":
                    GetProducer = "SamSung"; break;
                case "NSX03":
                    GetProducer = "Oppo"; break;
                case "NSX04":
                    GetProducer = "Nokia"; break;
                case "NSX05":
                    GetProducer = "HP"; break;
                case "NSX06":
                    GetProducer = "Asus"; break;
                case "NSX07":
                    GetProducer = "Acer"; break;
                case "NSX08":
                    GetProducer = "Macbook"; break;
            }

            return View(db);
        }
    }
}