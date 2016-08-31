/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Microsoft.Graph;
using System.Net.Http.Headers;
using System;

namespace OnAPPoint.Util
{
    public class GraphSDKHelper
    {
        private static GraphServiceClient graphClient = null;

        // Get an authenticated Microsoft Graph Service client.
        public static GraphServiceClient GetAuthenticatedClient()
        {
          if (graphClient == null)
          {
            graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                      string accessToken = await AppOnlyAuthProvider.Instance.GetUserAccessTokenAsync();

                          // Append the access token to the request.
                          requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

                          // Get event times in the current time zone.
                          requestMessage.Headers.Add("Prefer", "outlook.timezone=\"" + TimeZoneInfo.Local.Id + "\"");
                    }));
          }

          return graphClient;
        }

        public static void SignOutClient()
        {
            graphClient = null;
        }
    }
}