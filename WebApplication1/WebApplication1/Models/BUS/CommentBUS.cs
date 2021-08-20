using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnlineConnection;

namespace WebApplication1.Models.BUS
{
    public class CommentBUS
    {
        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var datase = new ShopOnlineConnectionDB();
            return datase;
        }
        public IEnumerable<CommentInformation> AllComments(string productId)
        {
            
            //Create Store Procedures
            //CREATE PROCEDURE usp_GetGuestCommentInfor @ProductId nvarchar(10)
            //AS
            //SELECT Comment.Id,Comment.UserEmail,Comment.CommentContent,Comment.[Date],AspNetUsers.FullName
            //FROM Comment Right JOIN AspNetUsers  ON Comment.UserEmail = AspNetUsers.Email
            //Where ProductId = @ProductId ORDER BY[Date] DESC
            //GO

            //EXEC usp_GetGuestCommentInfor @ProductId = 'SP01'

            return DatabaseConnection().Fetch<CommentInformation>(";EXEC usp_GetGuestCommentInfor @@ProductId = @0", productId);
        }

        public static void Create(Comment comment)
        {            
            DatabaseConnection().Insert(comment);
        }

        public static void Delete(Comment comment)
        {
            DatabaseConnection().Delete(comment);
        }

        public static void Edit(Comment comment ,int commentId)
        {
            DatabaseConnection().Update(comment,commentId);
        }
    }
}


