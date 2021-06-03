using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{
    //----------------Admin--------------
    public class AccountBUS
    {
        public static IEnumerable<AspNetUser> ListAccount()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<AspNetUser>("SELECT Id,Email,PhoneNumber,UserName FROM AspNetUsers");

        }

    }
}