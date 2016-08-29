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
    private void PrepareViewBag<T>(List<T> items, string itemName)
    {
      if (items == null)
      {
        ViewBag.AlertIcon = "glyphicon-exclamation-sign";
        ViewBag.AlertType = "danger";
        ViewBag.AlertMsg = "Es ist ein Fehler aufgetreten.";
        return;
      }

      List<string> jsonResult = new List<string>();
      foreach (object item in items)
      {
        jsonResult.Add(Util.JsonUtil.seralizeObject(item));
      }
      ViewBag.AlertIcon = "glyphicon-info-sign";
      ViewBag.AlertType = "info";
      ViewBag.AlertMsg = "Es wurden " + items.Count + " " + itemName + "einträge zurückgeliefert.";
      ViewBag.ItemName = itemName;
      ViewBag.Result = jsonResult;
    }

    public async Task<ActionResult> ListContacts()
    {
      string accessToken = (string)Session[Const.Settings.SessionAccessKey];
      List<Contact> list = await Util.GraphApiUtil.GetContacts(accessToken);
      PrepareViewBag(list, "Kontakt");
      return View(nameof(Index));
    }

    public async Task<ActionResult> ListCalendars()
    {
      string accessToken = (string)Session[Const.Settings.SessionAccessKey];
      List<Calendar> list = await Util.GraphApiUtil.GetCalendars(accessToken);
      PrepareViewBag(list, "Kalender");
      return View(nameof(Index));
    }

    public async Task<ActionResult> ListMessages()
    {
      string accessToken = (string)Session[Const.Settings.SessionAccessKey];
      List<Message> list = await Util.GraphApiUtil.GetMessages(accessToken);
      PrepareViewBag(list, "Email");
      return View(nameof(Index));
    }

    public async Task<ActionResult> ListEvents()
    {
      string accessToken = (string)Session[Const.Settings.SessionAccessKey];
      List<Event> list = await Util.GraphApiUtil.GetEvents(accessToken);
      PrepareViewBag(list, "Event");
      return View(nameof(Index));
    }

  }
}