using ImageFileHandler.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ImageFileHandler.Models
{
    public class ActivationParameters
    {
        public string Client { get; set; }
        public string CultureName { get; set; }
        public string FileGet { get; set; }
        public string FilePut { get; set; }
        public string ResourceId { get; set; }
        public string FileId { get; set; }

        public ActivationParameters(NameValueCollection activationParameters)
        {
            this.Client = activationParameters["client"];
            this.CultureName = activationParameters["cultureName"];
            this.FileGet = activationParameters["fileGet"];
            this.FilePut = activationParameters["filePut"];
            this.FileId = activationParameters["fileId"];
            this.ResourceId = activationParameters["resourceId"];

            if (string.IsNullOrEmpty(this.ResourceId) || string.IsNullOrEmpty(this.FilePut) || string.IsNullOrEmpty(this.FileGet))
            {
                throw new Exception("ResourceId and/or file locations are invalid - cannot get file.");
            }
        }


        public override String ToString()
        {
            String str = "";
            str += "Client: " + this.Client + "<br/>";
            str += "Culture Name: " + this.CultureName + "<br/>";
            str += "File GET: " + this.FileGet + "<br/>";
            str += "File PUT: " + this.FilePut + "<br/>";
            str += "ReourceId: " + this.ResourceId + "<br/>";
            str += "FileId: " + this.FileId + "<br/>";
            return str;
        }

        public static ActivationParameters LoadActivationParameters(HttpContext context)
        {
            ActivationParameters parameters = null;

            FormDataCookie cookie = new FormDataCookie(SettingsHelper.SavedFormDataName);
            if (context.Request.Form != null && context.Request.Form.AllKeys.Count<string>() != 0)
            {
                // get from current request's form data
                parameters = new ActivationParameters(context.Request.Form);
            }
            else if (cookie.Load() && cookie.IsLoaded && cookie.FormData.AllKeys.Count<string>() > 0)
            {
                // if form data does not exist, it must be because of the sign in redirection, at the time form data is saved in the cookie 
                parameters = new ActivationParameters(cookie.FormData);
                // clear the cookie after using it
                cookie.Clear();
            }
            else
            {
                parameters = (ActivationParameters)context.Session[SettingsHelper.SavedFormDataName];
            }
            return parameters;
        }

        public static void SaveActivationParameters(NameValueCollection activationParameters)
        {

        }
    }
}