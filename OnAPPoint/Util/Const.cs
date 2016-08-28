using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace OnAPPoint.Util
{
  public class Const
  {
    public static class Settings
    {
      public static string ClientId => ConfigurationManager.AppSettings["ClientID"];
      public static string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];

      public static string AzureADAuthority = @"https://login.microsoftonline.com/common";
      public static string LogoutAuthority = @"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=";
    }
 
    public static class GraphApi
    {

      public static string ApiResource = "https://graph.microsoft.com";
      public static string Endpoint = ApiResource + "/v1.0/me";
      public static string Contacts = Endpoint + "/contacts";

      public enum ItemType
      {
        Calendars,
        Events,
        Contacts,
        Messages
      }

    }
  }
}