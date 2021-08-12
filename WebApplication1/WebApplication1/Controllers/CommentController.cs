using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index(string productId)
        {
            ViewBag.productId = productId;

            return View(CommentBUS.CommentList(productId));
        }

        //[Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Editor")]
        [Authorize]
        // GET: Comment
        public ActionResult Create(Comment comment, string productId)
        {
            if (comment.ProductId == null)
            {
                return Redirect("/");
            }
            comment.Date = DateTime.Now;
            comment.UserEmail = User.Identity.Name;
            CommentBUS.Create(comment);

            return RedirectToAction("Details", "Shop", new { Id = productId });
        }
    }
}