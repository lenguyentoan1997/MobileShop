using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models.BUS;
using PagedList;
using System.Web.Script.Serialization;
using WebApplication1.Common;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CommentController : Controller
    {
        public ActionResult Index(string productId, int page = 1, int pageSize = 5)
        {
            ViewBag.ProductId = productId;

            var allComments = CommentModel.Instance.AllComments(productId);

            List<int> starList = new List<int>();

            List<int> countStarList = new List<int>();

            int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0;

            foreach (var item in allComments)
            {
                if (item.star != null)
                {
                    starList.Add((int)item.star);

                    switch (item.star)
                    {
                        case 1:
                            count1++;
                            break;
                        case 2:
                            count2++;
                            break;
                        case 3:
                            count3++;
                            break;
                        case 4:
                            count4++;
                            break;
                        case 5:
                            count5++;
                            break;
                    }
                }
            }
            //calculate point average star
            if (starList.Count != 0)
            {
                Helper.AveragePoint = (int)starList.Average();

                ProductModel.Instance.UpdateProductAveragePoint(Helper.AveragePoint, productId);
            }
            else
            {
                Helper.AveragePoint = 0;
            }
            //statistical number of stars
            countStarList.Add(count1);
            countStarList.Add(count2);
            countStarList.Add(count3);
            countStarList.Add(count4);
            countStarList.Add(count5);
            Helper.CountStarList = countStarList;

            var commentPageList = allComments.ToPagedList(page, pageSize);

            return View(commentPageList);
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
                case "Average":
                    comment.star = 3;
                    break;
                case "Good":
                    comment.star = 4;
                    break;
                case "Great":
                    comment.star = 5;
                    break;
                default:
                    break;
            }

            comment.Date = DateTime.Now;
            comment.UserEmail = User.Identity.Name;
            comment.CommentContent.Trim();
            CommentModel.Instance.Create(comment);

            return RedirectToAction("Details", "Product", new { Id = productId });
        }

        public JsonResult Edit(string commentModel)
        {
            var jsonComment = new JavaScriptSerializer().Deserialize<List<Comment>>(commentModel);

            foreach (var item in jsonComment)
            {
                item.CommentContent.Trim();
                CommentModel.Instance.Update(item.Id, item.CommentContent);
            }

            return Json(new { isStatus = true });
            //return RedirectToAction("Details", "Product", new { Id = ViewBag.ProductId });
        }


        public JsonResult Delete(string commentModel)
        {
            var jsonComment = new JavaScriptSerializer().Deserialize<List<Comment>>(commentModel);
            foreach (var item in jsonComment)
            {
                CommentModel.Instance.Delete(item.Id, item.CommentContent);
            }

            //return RedirectToAction("Details", "Product", new { Id = ViewBag.ProductId });
            return Json(new { isStatus = true });
        }
    }
}