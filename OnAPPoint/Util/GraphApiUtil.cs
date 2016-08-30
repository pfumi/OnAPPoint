using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnAPPoint.Util
{
  public static class GraphApiUtil
  {
    private static string Endpoint = Const.Settings.GraphApiResource + "/v1.0/me";
    private static string Contacts = "/contacts";
    private static string Calendars = "/calendars";
    private static string Messages = "/messages";
    private static string Events = "/events";

    public static string test()
    {
      Contact cont = new Contact();
      cont.GivenName = "Max";
      cont.Surname = "Muster";
      List<EmailAddress> email = new List<EmailAddress>();
      email.Add(new EmailAddress
      {
        Address = "master.muster@email.com",
        Name = "Max Muster"
      });
      cont.EmailAddresses = email;
      cont.MobilePhone = "0123456789";
      return JsonUtil.seralizeObject(cont);
    }

    private static async Task<List<T>> GetItem<T>(string query, string accessToken) where T : Entity
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(Endpoint + query);
        if (!response.IsSuccessStatusCode)
        {
          //TODO Logging
          return null;
        }
        string json = await response.Content.ReadAsStringAsync();
        JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());
        return result["value"].ToObject<List<T>>();
      }
    }

    public static async Task<List<Contact>> GetContacts(string accessToken)
    {
      return await GetItem<Contact>(Contacts, accessToken);
    }

    public static async Task<List<Calendar>> GetCalendars(string accessToken)
    {
      return await GetItem<Calendar>(Calendars, accessToken);
    }

    public static async Task<List<Message>> GetMessages(string accessToken)
    {
      return await GetItem<Message>(Messages, accessToken);
    }

    public static async Task<List<Event>> GetEvents(string accessToken)
    {
      return await GetItem<Event>(Events, accessToken);
    }
  }

}