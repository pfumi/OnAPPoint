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
      List<Contact> contacts = await Util.GraphApiUtil.GetContacts(accessToken);

      // Fehler bei Response
      if (contacts == null)
      {
        ViewBag.AlertIcon = "glyphicon-exclamation-sign";
        ViewBag.AlertType = "danger";
        ViewBag.AlertMsg = "Es ist ein Fehler aufgetreten.";
        return View(nameof(Index));
      }

      List<string> jsonResult = new List<string>();
      foreach (Contact contact in contacts)
      {
        jsonResult.Add(Util.JsonUtil.seralizeObject(contact));
      }
      ViewBag.AlertIcon = "glyphicon-info-sign";
      ViewBag.AlertType = "info";
      ViewBag.AlertMsg = "Es wurden " + contacts.Count + " Kontakte zurückgeliefert.";
      ViewBag.ItemName = "Kontakt";
      ViewBag.Result = jsonResult;
      return View(nameof(Index));
    }

  }
}