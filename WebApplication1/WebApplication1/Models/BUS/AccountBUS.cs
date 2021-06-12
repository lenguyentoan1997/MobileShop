using System;
using System.Collections.Generic;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{

    public class AccountBUS
    {
        //-------------- Share account for guest and admin-----------
        public static ShopOnlineConnectionDB Database()
        {
            var database = new ShopOnlineConnectionDB();
            return database;
        }

        //display details of account
        public static AspNetUser AccountDetails(String id)
        {
            return Database().SingleOrDefault<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id = @0", id);
        }

        //----------------Guest Account Managed By Admin--------------

        //display a list of guest account
        public IEnumerable<AspNetUser> ListGuestAccount()
        {
            return Database().Query<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id NOT IN (SELECT UserId FROM AspNetUserRoles)");
        }

        public void UpdateGuestAccount(AspNetUser aspNetUser, String id)
        {
            Database().Update(aspNetUser, id);
        }

        public void DeleteGuestAccount(AspNetUser aspNetUser)
        {
            Database().Delete(aspNetUser);
        }

        //----------------Admin Account Managed By Admin--------------
        public IEnumerable<AspNetUser> ListAdminAccocunt()
        {
            return Database().Query<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id IN (SELECT UserId FROM AspNetUserRoles)");
        }

        public void UpdateAdminAcocunt(AspNetUser aspNetUser, String id)
        {
            Database().Update(aspNetUser, id);
        }

        public void DeleteAdminAccount(AspNetUser aspNetUser)
        {
            Database().Delete(aspNetUser);
        }
    }
}