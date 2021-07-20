using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.BUS
{
    public class OrderBUS
    {
        ShopOnlineConnectionDB db = null;
        
        public OrderBUS()
        {
            db = new ShopOnlineConnectionDB();
        }
        public int Insert(Order order)
        {
            db.Insert(order);

            return order.ID;
        }
    }
}