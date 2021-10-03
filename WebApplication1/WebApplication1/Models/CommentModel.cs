using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using static WebApplication1.Models.Database;

namespace WebApplication1.Models.BUS
{
    public class CommentModel
    {
        private static readonly Lazy<CommentModel> lazy = new Lazy<CommentModel>(() => new CommentModel());
        public static CommentModel Instance
        {
            get => lazy.Value;          
        }
        private CommentModel() { }

        private static ShopOnlineConnectionDB DatabaseConnection()
        {
            var datase = new ShopOnlineConnectionDB();
            return datase;
        }

        public IEnumerable<CommentInformation> AllComments(string productId)
        {
            var executeSql = DatabaseConnection().Fetch<CommentInformation>
                (PetaPoco.Sql.Builder.Append(";EXEC usp_GetCommentInforByProductId @@ProductId = @0", productId));

            return executeSql;
        }

        public void Create(Comment comment)
        {
            DatabaseConnection().Insert(comment);
        }

        public void Delete(int commnetId, string commentContent)
        {
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("EXEC usp_CommentUpdateDelete @@Id = @0, @@CommentContent = @1,@@StatementType = 'DELETE'", commnetId, commentContent));
        }

        public void Update(int commentId, string commentContent)
        {
            DatabaseConnection().Execute(PetaPoco.Sql.Builder.Append("EXEC usp_CommentUpdateDelete @@Id = @0, @@CommentContent = @1, @@StatementType = 'UPDATE'", commentId, commentContent));
        }
    }
}


