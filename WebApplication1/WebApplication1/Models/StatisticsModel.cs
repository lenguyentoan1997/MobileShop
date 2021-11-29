using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using static WebApplication1.Models.Database;

namespace WebApplication1.Models
{
    public class StatisticsModel
    {
        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var db = new ShopOnlineConnectionDB();
            return db;
        }

        /*
         * Get all information in order by date
         */
        public static IEnumerable<OrderInformationByDate> GetOrderByDateFromDB(DateTime? dateFrom, DateTime? dateTo)
              => DatabaseConnection().Query<OrderInformationByDate>(PetaPoco.Sql.Builder.Append(";Exec sp_GetOrderDataByDate @@DateFrom = @0 , @@DateTo = @1", dateFrom, dateTo));

        /*
         * Get activity of the day all client
         */
        public static IEnumerable<tblAccountOrderCommentActivitylog> GetActivityDateNow(DateTime dateFrom, DateTime dateTo)
              => DatabaseConnection().Query<tblAccountOrderCommentActivitylog>(PetaPoco.Sql.Builder.Append(";Exec usp_GetActivityLogByDate @@DateFrom=@0,@@DateTo=@1", dateFrom, dateTo));
    }
}