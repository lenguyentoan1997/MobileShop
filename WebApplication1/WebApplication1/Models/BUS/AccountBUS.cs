﻿using System;
using System.Collections.Generic;
using System.Reflection;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{

    public class AccountBUS
    {

        //-------------- Share account for guest and admin-----------
        private static ShopOnlineConnectionDB Database()
        {
            var database = new ShopOnlineConnectionDB();
            return database;
        }

        //display details of account
        public static AspNetUser AccountDetails(string id)
        {
            return Database().SingleOrDefault<AspNetUser>("SELECT * FROM udf_GetAccountById(@0)", id);
        }

        //----------------Guest Account Managed By Admin--------------
        //display a list of guest account
        //public string InsertForFacebook(AspNetUser entity)
        //{
        //    var db = new ShopOnlineConnectionDB();
        //    //LingQ var user = db.Users.SingleOrDefault(x=>x.UserName == entity.UserName);
        //    var user = db.SingleOrDefault<AspNetUser>("SELECT * FROM AspNetUsers WHERE UserName = @0", entity.UserName);
        //    if(user == null)
        //    {
        //        db.Insert("dbo.AspNetUsers",entity);
        //        return entity.Id;
        //    }
        //    else
        //    {
        //        return entity.Id;
        //    }

        //}
        public IEnumerable<AspNetUser> ListGuestAccount()
        {
            return Database().Query<AspNetUser>(" SELECT * FROM vw_GuestAccount");
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
            return Database().Query<AspNetUser>("SELECT * FROM vw_AdminAccount");
        }

        public void UpdateAdminAcocunt(AspNetUser aspNetUser, string id)
        {
            Database().Update(aspNetUser, id);
        }

        public void DeleteAdminAccount(AspNetUser aspNetUser)
        {
            Database().Delete(aspNetUser);
        }

    }
}