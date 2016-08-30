﻿using Microsoft.Graph;
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

    private static string Endpoint = Const.Settings.GraphApiResource + "/v1.0/users/f099d2f3-aab5-4ceb-a9e6-c45ad01a8212";
    private static string Contacts = "/contacts";
    private static string Calendars = "/calendars";
    private static string Events = "/events";
    private static string Messages = "/messages";

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

    private static async Task<List<T>> GetItem<T>(string query, string accessToken)
    {
      List<T> items = new List<T>();
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.GetAsync(Endpoint + query);
        string json = await response.Content.ReadAsStringAsync();
        JObject result = JObject.Parse(await response.Content.ReadAsStringAsync());
        if (result["error"] != null)
        {
          //TODO do something, logging at least
          return items;
        }
        foreach (JObject obj in result["value"]) {
          items.Add(obj.ToObject<T>());
        }
      }
      return items;
    }

    public static async Task<List<Contact>> GetContacts(string accessToken)
    {
      return await GetItem<Contact>(Contacts, accessToken);
    }

  }

}