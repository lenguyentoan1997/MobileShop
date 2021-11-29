using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{
    public class OrderModel
    {
        ShopOnlineConnectionDB db = null;

        public OrderModel()
        {
            db = new ShopOnlineConnectionDB();
        }

        public int Insert(Order order)
        {  
            db.Insert("dbo.[Order]",order);

            return order.ID;
        }


    }
}