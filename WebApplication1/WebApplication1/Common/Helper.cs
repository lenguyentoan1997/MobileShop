using ShopOnlineConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Controllers;
using WebApplication1.Models;
using static WebApplication1.Controllers.AccountController;


namespace WebApplication1.Common
{
    public static class Helper
    {
        public static bool IsSocialMediaLogin { get; set; }
        public static int AveragePoint { get; set; }

        public static List<int> CountStarList { get; set; }

        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
            {
                return fullNameClaim.Value;
            }

            return "";
        }

        public static string FormatTime(DateTime commentDate)
        {
            var seconds = Math.Floor((DateTime.Now - commentDate).TotalSeconds);

            var interval = Math.Floor(seconds / 31536000);

            if (interval >= 1)
            {
                return interval + " years";
            }
            interval = Math.Floor(seconds / 2592000);
            if (interval >= 1)
            {
                return interval + " months";
            }
            interval = Math.Floor(seconds / 86400);
            if (interval >= 1)
            {
                return interval + " days";
            }
            interval = Math.Floor(seconds / 3600);
            if (interval >= 1)
            {
                return interval + " hours";
            }
            interval = Math.Floor(seconds / 60);
            if (interval >= 1)
            {
                return interval + " minutes";
            }
            return Math.Floor(seconds) + " seconds";
        }
    }
}