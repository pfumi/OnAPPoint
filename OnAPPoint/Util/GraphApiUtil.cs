using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using static OnAPPoint.Util.GraphApiConst;

namespace OnAPPoint.Util
{
  public static class GraphApiUtil<T> where T : OutlookItem
  {

    private static JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    public static string test()
    {
      Contact cont = new Contact();
      cont.GivenName = "Max";
      cont.Surname = "Muster";
      EmailAddress email = new EmailAddress();
      email.Name = "Max Muster";
      email.Address = "master.muster@email.com";
      cont.EmailAddresses.Add(email);
      cont.MobilePhone = "0123456789";
      string json = JsonConvert.SerializeObject(cont, Newtonsoft.Json.Formatting.Indented, DefaultJsonSerializerSettings);
      return json;
    }

    private static async Task<T> GetItem(string query)
    {
      T item = default(T);
      using (var client = new HttpClient())
      {
        //TODO Authorization
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", null);
        var response = await client.GetAsync(query);
        string json = await response.Content.ReadAsStringAsync();
        JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (result["error"] == null)
        {
          //TODO do something, logging at least
        }
        List<T> items = new List<T>();
        foreach (JObject obj in result["value"]) {
          items.Add(obj.ToObject<T>());
        }
      }
      return item;
    }

    public static async Task<T> GetItem(ItemType itemType)
    {
      T item = default(T);
      switch (itemType)
      {
        case ItemType.Contacts:
          item = await GetItem(Contacts);
          break;
        case ItemType.Calendars:
        case ItemType.Events:
        case ItemType.Messages:
          throw new NotSupportedException("GraphItem not supported.");
      }
      return item;
    }

    public static void PostItem(T item)
    {
      //TODO
    }

    public static void PatchItem(T item)
    {
      //TODO
    }

    public static void DeleteItem(T item)
    {
      //TODO
    }
  }

  public class GraphApiConst
  {

    private static string Endpoint = "https://graph.microsoft.com/v1.0/me";

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