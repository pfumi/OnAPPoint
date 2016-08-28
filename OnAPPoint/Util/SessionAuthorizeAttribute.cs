using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnAPPoint.Util
{
  public class SessionAuthorizeAttribute : AuthorizeAttribute
  {

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
      return httpContext.Session[Const.Settings.SessionAccessKey] != null;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
      filterContext.Result = new RedirectResult("/Home/Login");
    }
  }
}