using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{
    public class CommentBUS
    {
        public static void Create(Comment comment)
        {
            var db = new ShopOnlineConnectionDB();
            db.Insert(comment);
        }

        public static IEnumerable<Comment> CommentList(string productId)
        {
            var db = new ShopOnlineConnectionDB();

            return db.Query<Comment>("SELECT * FROM Comment WHERE ProductId = @0", productId);
        }
    }
}