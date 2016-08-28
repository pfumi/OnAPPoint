using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnAPPoint.Controllers
{
  public class GraphApiController : Controller
  {
    // GET: GraphTest
    public ActionResult Index()
    {
      // Provisorische Prüfung ob User eingeloggt
      if (Session["AccessToken"] == null)
      {
        return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
      }
      return View();
    }
  }
}