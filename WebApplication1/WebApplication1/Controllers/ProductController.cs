using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }

        // GET: Shop/Details/5
        public ActionResult Details(String id)
        {
            var db = ProductBUS.ChiTiet(id);
            return View(db);
        }

        public ActionResult AllPhone(int page = 1, int pageSize = 4)
        {
            var db = ProductBUS.AllPhone().ToPagedList(page, pageSize);
            return View(db);
        }

        public ActionResult AllLaptop(int page = 1, int pageSize = 4)
        {
            var db = ProductBUS.AllLaptop().ToPagedList(page, pageSize);
            return View(db);
        }

        public ActionResult SimilarProducts(string producerCode, int price)
        {
            var db = ProductBUS.SimilarProducts(producerCode, price);
            return View(db);
        }

        // GET: Shop/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shop/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shop/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shop/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
