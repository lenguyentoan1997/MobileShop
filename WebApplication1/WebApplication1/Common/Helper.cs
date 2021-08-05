using System;
using System.Collections.Generic;
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
        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
            {
                return fullNameClaim.Value;
            }

            return "";
        }

        public static bool IsloginLocal(this System.Security.Principal.IPrincipal usr)
        {

            if (IsSocialMediaLogin == true)
            {
                return false;
            }
            return true;
        }

    }
}