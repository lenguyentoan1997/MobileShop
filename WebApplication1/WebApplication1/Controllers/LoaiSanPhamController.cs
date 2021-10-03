using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class LoaiSanPhamController : Controller
    {
        // GET: LoaiSanPham
        public ActionResult Index(String id, int page = 1, int pagesize = 3)
        {
            var ds = LoaiSanPhamModel.Instance.ChiTiet(id).ToPagedList(page,pagesize);
            return View(ds);
        }
    }
}