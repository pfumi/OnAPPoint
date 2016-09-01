/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Microsoft.Graph;
using OnAPPoint.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnAppoint.Models
{
  public class EventsService
  {
    // Get calendars
    public async Task<List<ResultsItem>> GetCalendars(GraphServiceClient graphClient, string userID = null, string groupID = null)
    {
      List<ResultsItem> items = new List<ResultsItem>();

      // default: matthias.pfurtscheller@pfurml.onmicrosoft.com TODO: ändern
      userID = userID ?? "f099d2f3-aab5-4ceb-a9e6-c45ad01a8212";
      // default: Mitarbeitergruppe in matthias.pfurtscheller@pfurml.onmicrosoft.com TODO: ändern
      groupID = groupID ?? "AAMkAGRjM2JiYjUxLTdkMzQtNDBiNy1hYTE0LTY1NTg4YjNkMjhiMABGAAAAAABURYew-Y1vRoSHy1UQetQ6BwCzMNTQWwFmRoqNfNysC6efAAAAAAEGAACzMNTQWwFmRoqNfNysC6efAAAEAbSiAAA=";

      ICalendarGroupCalendarsCollectionPage calendars = await graphClient.Users[userID].CalendarGroups[groupID].Calendars.Request().GetAsync();
      foreach (Calendar cal in calendars)
      {
        items.Add(new ResultsItem
        {
          Display = cal.Name,
          Id = cal.Id,
          Type = cal.GetType().Name
      });

        // Get Events of each calendar within the desired timerange
        List<QueryOption> options = new List<QueryOption>();
        options.Add(new QueryOption("StartDateTime", "2015-11-08T19:00:00.0000000"));
        options.Add(new QueryOption("EndDateTime", "2016-11-08T19:00:00.0000000"));

        ICalendarCalendarViewCollectionPage calendarview = await graphClient.Users[userID].CalendarGroups[groupID].Calendars[cal.Id].CalendarView.Request(options).Select("subject,start,end,showAs").GetAsync();
        foreach (Event ev in calendarview)
        {
          items.Add(new ResultsItem
          {
            Display = ev.Subject,
            Id = ev.Id,
            Type = ev.GetType().Name,
            Properties = new Dictionary<string, object>
                            {
                                { "start", ev.Start.DateTime },
                                { "end", ev.End.DateTime }
                            }
          });
        }
      }

      return items;
    }
  }
}