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

        public IEnumerable<CommentInformation> AllComments(string productId)
        {
            var db = new ShopOnlineConnectionDB();

            //Create Store Procedures
            //CREATE PROCEDURE usp_GetGuestCommentInfor @ProductId nvarchar(10)
            //AS
            //SELECT Comment.Id,Comment.UserEmail,Comment.CommentContent,Comment.[Date],AspNetUsers.FullName
            //FROM Comment Right JOIN AspNetUsers  ON Comment.UserEmail = AspNetUsers.Email
            //Where ProductId = @ProductId ORDER BY[Date] DESC
            //GO

            //EXEC usp_GetGuestCommentInfor @ProductId = 'SP01'

            return db.Fetch<CommentInformation>(";EXEC usp_GetGuestCommentInfor @@ProductId = @0", productId);
        }
    }
}


