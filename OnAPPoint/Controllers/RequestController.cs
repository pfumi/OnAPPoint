using Microsoft.Graph;
using OnAppoint.Models;
using OnAPPoint.Models;
using OnAPPoint.Util;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnAPPoint.Controllers
{
  public class RequestController : Controller
  {
    EventsService eventsService = new EventsService();

    private void PrepareViewBag(ResultsViewModel results)
    {
      if (results.Items.Count == 0)
      {
        ViewBag.AlertIcon = "glyphicon-exclamation-sign";
        ViewBag.AlertType = "danger";
        ViewBag.AlertMsg = "Es ist ein Fehler aufgetreten.";
        return;
      }

      ViewBag.AlertIcon = "glyphicon-info-sign";
      ViewBag.AlertType = "success";
      ViewBag.AlertMsg = "Es " + (results.Items.Count == 1 ? "wurde " : "wurden ") + results.Items.Count + (results.Items.Count == 1 ? " Eintrag" : " Einträge") + " zurückgeliefert.";
    }


    // GET: Request
    public async Task<ActionResult> Index()
    {
      ResultsViewModel results = new ResultsViewModel();
      try
      {
        // Initialize the GraphServiceClient.
        GraphServiceClient graphClient = GraphSDKHelper.GetAuthenticatedClient();

        // Get users.
        results.Items = await eventsService.GetCalendars(graphClient);
      }
      catch (ServiceException se)
      {
        if (se.Error.Message == Resource.Error_AuthChallengeNeeded) return new EmptyResult();
        return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, Request.RawUrl, se.Error.Code, se.Error.Message) });
      }
      catch (Exception e)
      {
        return RedirectToAction("Index", "Error", new { message = string.Format(Resource.Error_Message, e.Source, e.HResult, e.Message) });
      }

      ViewBag.Title = "Friseure";
      PrepareViewBag(results);

      return View(results);
    }

    // GET: Request/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: Request/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Request/Create
    [HttpPost]
    public ActionResult Create(FormCollection collection)
    {
        try
        {
            // TODO: Add insert logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: Request/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: Request/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
        try
        {
            // TODO: Add update logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }

    // GET: Request/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: Request/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
        try
        {
            // TODO: Add delete logic here

            return RedirectToAction("Index");
        }
        catch
        {
            return View();
        }
    }
  }
}
