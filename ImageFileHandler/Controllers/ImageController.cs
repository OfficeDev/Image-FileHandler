using ImageFileHandler.Models;
using ImageFileHandler.Utils;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ImageFileHandler.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index()
        {
            //get activation parameters off the request
            ActivationParameters parameters = ActivationParameters.LoadActivationParameters(System.Web.HttpContext.Current);

            //try to get access token using refresh token
            var token = await GetAccessToken(parameters.ResourceId);

            //get the image
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);
            var imgBytes = await client.GetByteArrayAsync(parameters.FileGet);
            
            //return the image as a base64 string
            ViewData["img"] = "data:image/png;base64, " + Convert.ToBase64String(imgBytes);
            ViewData["resource"] = parameters.ResourceId;
            ViewData["refresh_token"] = token.RefreshToken;
            ViewData["file_put"] = parameters.FilePut;
            ViewData["return_url"] = parameters.FilePut.Substring(0, parameters.FilePut.IndexOf("_vti_bin"));
            return View();
        }

        public ActionResult Preview()
        {
            return View();
        }

        private async Task<AuthenticationResult> GetAccessToken(string resource)
        {
            AuthenticationContext context = new AuthenticationContext(SettingsHelper.AzureADAuthority);
            var clientCredential = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);
            AuthenticationResult result = (AuthenticationResult)this.Session[SettingsHelper.UserTokenCacheKey];
            return await context.AcquireTokenByRefreshTokenAsync(result.RefreshToken, clientCredential, resource);
        }
    }
}