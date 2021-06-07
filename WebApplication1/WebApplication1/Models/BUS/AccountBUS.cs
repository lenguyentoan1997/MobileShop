using System.Collections.Generic;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{
    //----------------Admin--------------
    public class AccountBUS
    {

        public static IEnumerable<AspNetUser> ListGuestAccount()
        {
            var db = new ShopOnlineConnectionDB();
            return db.Query<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id NOT IN (SELECT UserId FROM AspNetUserRoles)");

        }

    }
}