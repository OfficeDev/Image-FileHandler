using ImageFileHandler.Models;
using ImageFileHandler.Utils;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ImageFileHandler.Controllers
{
    public class SaveController : ApiController
    {
        [Route("api/Save/")]
        public string Get()
        {
            return "";
        }

        [Route("api/Save/")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]SaveModel value)
        {
            //get an access token using the refresh token posted in the request
            var token = await GetAccessToken(value.resource, value.token);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.AccessToken);

            //convert base64 image string into byte[] and then stream
            byte[] bytes = Convert.FromBase64String(value.image);
            using (Stream stream = new MemoryStream(bytes))
            {
                //prepare the content body
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(value.fileput, fileContent);
            }

            return Request.CreateResponse<bool>(HttpStatusCode.OK, true);
        }

        private async Task<AuthenticationResult> GetAccessToken(string resource, string refresh)
        {
            AuthenticationContext context = new AuthenticationContext(SettingsHelper.AzureADAuthority);
            var clientCredential = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.ClientSecret);
            return await context.AcquireTokenByRefreshTokenAsync(refresh, clientCredential, resource);
        }
    }
}
