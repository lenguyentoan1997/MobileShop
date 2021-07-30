using Facebook;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShopOnlineConnection;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.BUS;

namespace WebApplication1.Controllers
{
    public class ExternalLoginController : Controller
    {   //code để hiểu rõ về nguyên lý hoạt động ExternalLogin
        private Uri _redirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");

                return uriBuilder.Uri;
            }
        }
        // GET: ExternalLogin
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = _redirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult FacebookCallback(string code)
        {
            var facebookClient = new FacebookClient();
            dynamic result = facebookClient.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = _redirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                facebookClient.AccessToken = accessToken;
                //get the user information, like email, first name,...
                dynamic facebookAccountInfor = facebookClient.Get("me?fields=first_name,middle_name,last_name,id,email");
                var user = new AspNetUser();
                user.Email = facebookAccountInfor.email;
                user.UserName = facebookAccountInfor.email;
                user.FullName = facebookAccountInfor.first_name + " " + facebookAccountInfor.middle_name + " " + facebookAccountInfor.last_name;
                user.Id = facebookAccountInfor.id;
                var resultInsert = new AccountBUS().InsertForFacebook(user);
                if (resultInsert != null)
                {
 

                }

            }
            return Redirect("/");
        }
    }
}