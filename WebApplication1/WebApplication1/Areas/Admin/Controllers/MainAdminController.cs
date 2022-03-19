using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;
using static WebApplication1.Models.Database;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class MainAdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin/MainAdmin
        public ActionResult Index()
        {
            return View();
        }

        /*
         * Statistics
         * List of all products sold
         */
        private static List<Statistics> s_listProductSold;

        //Check the date of the request from the customer sent
        public DateTime[] CheckInputDate(DateTime? dateFrom, DateTime? dateTo)
        {
            DateTime[] dateTimes = new DateTime[2];

            if (dateFrom == null)
            {
                if (dateTo == null)
                {
                    //Date From == null , Date To == null
                    //Date From and Date To equal null then will dateFrom get current date minus 10 days,dateTo get current date 
                    dateTimes[0] = DateTime.Now.AddDays(-10);
                    dateTimes[1] = DateTime.Now;
                }
                else
                {
                    //Date From ==null, Date To != null
                    dateTimes[0] = dateTo.Value.AddDays(-10);
                    dateTimes[1] = dateTo.Value;
                }

            }
            else if (dateFrom != null)
            {
                if (dateTo == null)
                {
                    //Date From != null , Date To == null
                    dateTimes[0] = dateFrom.Value;
                    dateTimes[1] = dateFrom.Value.AddDays(+10);
                }
                else
                {
                    //Date From != null , Date To != null
                    dateTimes[0] = (DateTime)dateFrom;
                    dateTimes[1] = (DateTime)dateTo;
                }
            }
            else
            {
                //Date From = Date To
                dateTimes[0] = dateFrom.Value.AddHours(-12);
                dateTimes[1] = dateTo.Value.AddHours(+12);
            }

            return dateTimes;
        }
        //Get all products sold by date from database
        public void GetSalesDataByOrderFromDate(DateTime? dateFrom, DateTime? dateTo)
        {
            var date = CheckInputDate(dateFrom, dateTo);

            List<OrderInformationByDate> listAllOrder = new List<OrderInformationByDate>();
            //Get all information in order by dateFrom -> dateTo
            listAllOrder.AddRange(StatisticsModel.GetOrderByDateFromDB(date[0], date[1]));

            //Check the productId,if it is the same then sum quantity
            var listByProductIdAndQuantity = listAllOrder.GroupBy(item => item.ProductID).Select(group => new { ProductId = group.Key, Quantity = group.Sum(item => item.Quantity) }).ToArray();

            //Create order list to statistics productId and quantity sold
            List<Statistics> orderListStatistics = new List<Statistics>();
            foreach (var item in listByProductIdAndQuantity)
            {
                Statistics statisticsOrderByProductId = new Statistics(item.ProductId, (int)item.Quantity);

                orderListStatistics.Add(statisticsOrderByProductId);
            }

            s_listProductSold = new List<Statistics>();
            //Add all products sold by date on s_listProductSold
            s_listProductSold.AddRange(orderListStatistics);
        }

        //Statistics of sold orders by the date selected by the client
        //POST
        [HttpPost]
        public ActionResult Statistics(DateTime? dateFrom, DateTime? dateTo)
        {
            GetSalesDataByOrderFromDate(dateFrom, dateTo);

            return RedirectToAction("Index");
        }

        //Statistics of sold orders display view.
        //GET: Admin/MainAdmin/Statistics
        public ActionResult Statistics()
        {
            if (s_listProductSold != null)
            {
                return View(s_listProductSold);
            }
            else
            {
                DateTime dateFrom = DateTime.Now.AddDays(-10);
                DateTime dateTo = DateTime.Now;

                GetSalesDataByOrderFromDate(dateFrom, dateTo);

                return View(s_listProductSold);
            }
        }

        /*
         * daily feed
         */
        public ActionResult Feed()
        {
            DateTime dateFrom = DateTime.Today;
            DateTime dateTo = dateFrom.AddHours(+23).AddMinutes(+59).AddSeconds(+59).AddMilliseconds(+59);

            IEnumerable<tblAccountOrderCommentActivitylog> feed = StatisticsModel.GetActivityDateNow(dateFrom,dateTo);
         
            return View(feed);
        }

        // GET: Admin/MainAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/MainAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MainAdmin/Create
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

        // GET: Admin/MainAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/MainAdmin/Edit/5
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

        // GET: Admin/MainAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/MainAdmin/Delete/5
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
