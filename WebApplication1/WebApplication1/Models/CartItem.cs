using ShopOnlineConnection;
using System;

namespace WebApplication1.Models.BUS
{
    [Serializable]
    public class CartItem
    {
        public SanPham Product { set; get; }
        public int Quantity { set; get; }
       
    }
}