using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
      ViewBag.Result = null;
      return View();
    }

    public async Task<ActionResult> ListContacts()
    {
      string accessToken = (string)Session["AccessToken"];
      List<Contact> result = await Util.GraphApiUtil.GetContacts(accessToken);
      List<string> jsonResult = new List<string>();
      foreach (Contact r in result)
      {
        jsonResult.Add(Util.JsonUtil.seralizeObject(result));
      }
      ViewBag.ItemName = "Contact";
      ViewBag.Result = jsonResult;
      return View(nameof(Index));
    }
  }
}