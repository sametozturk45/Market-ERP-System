using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace MarketKasaSistemi.Web.Filters
{
    public class GirisKontrol : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] != null)
            {
                Kullanici giris = filterContext.HttpContext.Session["User"] as Kullanici;
                if (giris != null)
                    return;
            }
            filterContext.Result = new HttpUnauthorizedResult();
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result != null && filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Controller.ControllerContext.HttpContext.Response.Redirect("~/");
            }
        }
    }
}