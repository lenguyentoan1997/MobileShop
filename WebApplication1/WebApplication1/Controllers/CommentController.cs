using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using PagedList;
using System.Web.Script.Serialization;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index(string productId, int page = 1, int pageSize = 5)
        {
            ViewBag.ProductId = productId;
            CommentBUS commentBUS = new CommentBUS();
            var allComments = commentBUS.AllComments(productId).ToPagedList(page, pageSize);

            return View(allComments);
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Editor")]
        [Authorize]
        // GET: Comment
        public ActionResult Create(Comment comment, string productId, string qualityEvalution)
        {
            if (comment.ProductId == null)
            {
                return Redirect("/");
            }

            switch (qualityEvalution)
            {
                case "Bad":
                    comment.star = 1;
                    break;
                case "Worse":
                    comment.star = 2;
                    break;
            }

            comment.Date = DateTime.Now;
            comment.UserEmail = User.Identity.Name;
            comment.CommentContent.Trim();
            CommentBUS.Create(comment);

            return RedirectToAction("Details", "Shop", new { Id = productId });
        }

        public JsonResult Edit(string commentModel)
        {
            var jsonComment = new JavaScriptSerializer().Deserialize<List<Comment>>(commentModel);
            var commentBUS = new CommentBUS();
            foreach (var item in jsonComment)
            {
                item.CommentContent.Trim();
                commentBUS.Update(item.Id, item.CommentContent);
            }

            return Json(new { status = true });
        }

        [Authorize]
        public ActionResult Delete(Comment comment, string productId)
        {
            CommentBUS.Delete(comment);

            return RedirectToAction("Details", "Shop", new { Id = productId });
        }
    }
}