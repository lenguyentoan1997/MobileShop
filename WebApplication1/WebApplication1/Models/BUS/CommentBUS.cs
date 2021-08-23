using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
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
            var executeSql = DatabaseConnection().Fetch<CommentInformation>
                (PetaPoco.Sql.Builder.Append(";EXEC usp_GetGuestCommentInfor @@ProductId = @0", productId));

            return executeSql;
        }

        public static void Create(Comment comment)
        {
            DatabaseConnection().Insert(comment);
        }

        public static void Delete(Comment comment)
        {
            DatabaseConnection().Delete(comment);
        }

        public void Update(int commentId, string commentContent)
        {          
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("EXEC usp_CommentUpdateDelete @@Id = @0, @@CommentContent = @1, @@StatementType = 'UPDATE'", commentId, commentContent));
        }
    }
}


