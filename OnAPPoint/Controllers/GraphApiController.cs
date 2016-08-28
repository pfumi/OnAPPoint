using OnAPPoint.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnAPPoint.Controllers
{
  [SessionAuthorize]
  public class GraphApiController : Controller
  {

    public ActionResult Index()
    {
      return View();
    }

    public async Task<ActionResult> ListContacts()
    {
      string accessToken = (string)Session[Const.Settings.SessionAccessKey];
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