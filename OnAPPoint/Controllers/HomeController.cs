using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnAPPoint.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnAPPoint.Controllers
{
  public class HomeController : Controller
  {

    // The URL that auth should redirect to after a successful login.
    Uri loginRedirectUri => new Uri(Url.Action(nameof(Authorize), "Home", null, Request.Url.Scheme));

    // The URL to redirect to after a logout.
    Uri logoutRedirectUri => new Uri(Url.Action(nameof(Index), "Home", null, Request.Url.Scheme));

    public ActionResult Index()
    {
      return View();
    }

    public ActionResult Logout()
    {
      Session.Clear();
      return Redirect(Const.Settings.LogoutAuthority + logoutRedirectUri.ToString());
    }

    public async Task<ActionResult> Login()
    {
      if (string.IsNullOrEmpty(Const.Settings.ClientId) || string.IsNullOrEmpty(Const.Settings.ClientSecret))
      {
        ViewBag.Message = "Please set your client ID and client secret in the Web.config file";
        return View();
      }

      var authContext = new AuthenticationContext(Const.Settings.AzureADAuthority);

      // Generate the parameterized URL for Azure login.
      Uri authUri = await authContext.GetAuthorizationRequestUrlAsync(
          Const.Settings.GraphApiResource,
          Const.Settings.ClientId,
          loginRedirectUri,
          UserIdentifier.AnyUser,
          null);

      // Redirect the browser to the login page, then come back to the Authorize method below.
      return Redirect(authUri.ToString());
    }

    public async Task<ActionResult> Authorize()
    {
      var authContext = new AuthenticationContext(Const.Settings.AzureADAuthority);


      // Get the token.
      var authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
          Request.Params["code"],                                         // the auth 'code' parameter from the Azure redirect.
          loginRedirectUri,                                               // same redirectUri as used before in Login method.
          new ClientCredential(Const.Settings.ClientId, Const.Settings.ClientSecret), // use the client ID and secret to establish app identity.
          Const.Settings.GraphApiResource);

      // Save the token in the session.
      Session.Add("AccessToken", authResult.AccessToken);

      return RedirectToAction(nameof(Index));

    }
  }
}