using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using WebApplication1.Controllers;

namespace WebApplication1.Common
{
    public static class Helper
    {
        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
            {
                return fullNameClaim.Value;
            }

            return "";
        }
       
    }
}