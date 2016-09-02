/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;

namespace OnAPPoint.Util
{
    public sealed class AppOnlyAuthProvider : IAuthProvider
    {
        // Properties used to get and manage an access token.
        private string appId = ConfigurationManager.AppSettings["ClientID"];
        //private string pfxFilePath = ConfigurationManager.AppSettings["ida:pfxFilePath"];
        //private string x509CertificatePW = ConfigurationManager.AppSettings["ida:x509CertificatePW"];
        private string tenant = ConfigurationManager.AppSettings["ida:tenant"];

        private string accessToken = null;

        private static readonly AppOnlyAuthProvider instance = new AppOnlyAuthProvider();
        private AppOnlyAuthProvider() { }

        public static AppOnlyAuthProvider Instance
        {
            get
            {
                return instance;
            }
        }

        // Gets an access token. First tries to get the token from the token cache.
        public async Task<string> GetUserAccessTokenAsync()
        {
// TODO: AppOnly accessToken - prüfen, ob einer schon vorhanden ist und auch gültig ist
//            if (accessToken != null)
//              return (accessToken);

            X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            certStore.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certCol = certStore.Certificates.Find(
              X509FindType.FindByThumbprint,
              "38661500B0431DC2D7C73F02AE9FEC85D6479E95",
              false
              );
            X509Certificate2 cert = certCol[0];

      /*
            var certFile = System.IO.File.OpenRead(pfxFilePath);
            var certBytes = new byte[certFile.Length];
            certFile.Read(certBytes, 0, (int)certFile.Length);
            var cert = new X509Certificate2(
                certBytes,
                x509CertificatePW,
                X509KeyStorageFlags.Exportable |
                X509KeyStorageFlags.MachineKeySet |
                X509KeyStorageFlags.PersistKeySet
                );
      */

            ClientAssertionCertificate cac = new ClientAssertionCertificate(appId, cert);
            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenant, false);
            var authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com", cac);

            accessToken = authResult.AccessToken;

            return (accessToken);
        }
    }
}
