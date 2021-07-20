using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.BUS
{
    public class OrderDetailBUS
    {
        ShopOnlineConnectionDB db = null;
        public OrderDetailBUS()
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