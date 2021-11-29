using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using static WebApplication1.Models.Database;

namespace WebApplication1.Models.BUS
{
    public class OrderDetailModel
    {
        static ShopOnlineConnectionDB db = null;
        public OrderDetailModel()
        {
            db = new ShopOnlineConnectionDB();
        }
        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                db.Insert(orderDetail);

                return true;
            }
            catch
            {
                return false;
            }
        }
     
    }
}