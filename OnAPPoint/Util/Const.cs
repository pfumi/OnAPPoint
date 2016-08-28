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
      public static string SessionAccessKey = "AccessKey";

      public static string AzureADAuthority = @"https://login.microsoftonline.com/common";
      public static string LogoutAuthority = @"https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=";

      public static string GraphApiResource = "https://graph.microsoft.com";
    }
 
  }
}