using System;
using System.Collections.Generic;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{

    public class AccountBUS
    {
        //----------------Guest Account Managed By Admin--------------

        public static ShopOnlineConnectionDB Database()
        {
            var database = new ShopOnlineConnectionDB();
            return database;
        }

        //display a list of guest account
        public IEnumerable<AspNetUser> ListGuestAccount()
        {
            return Database().Query<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id NOT IN (SELECT UserId FROM AspNetUserRoles)");
        }

        //display details of guest account
        public static AspNetUser GuestAccountDetails(String id)
        {
            return Database().SingleOrDefault<AspNetUser>("SELECT * FROM AspNetUsers WHERE Id = @0", id);
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

    }
}