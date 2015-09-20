using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ImageFileHandler.Utils
{
    public class SettingsHelper 
    {
        public const string SavedFormDataName = "FILEHANDLER_FORMDATA";
        public const string UserTokenCacheKey = "APP_TOKEN";

        public static string ClientId {
            get { return ConfigurationManager.AppSettings["ida:ClientId"]; }
        }
        
        public static string ClientSecret {
            get { return ConfigurationManager.AppSettings["ida:ClientSecret"]; }
        }

        public static string AzureADAuthority {
            get { return "https://login.microsoftonline.com/common/"; }
        }

        public static string AuthorizationUri {
            get { return "https://login.microsoftonline.com/{0}/"; }
        }

        public static string AppBaseUrl {
            get { return ConfigurationManager.AppSettings["ida:AppBaseUrl"]; }
        }

        public static string ClaimTypeObjectIdentifier {
            get { return "http://schemas.microsoft.com/identity/claims/objectidentifier"; }
        }

        public static string ClaimTypeTenantId {
            get { return "http://schemas.microsoft.com/identity/claims/tenantid"; }
        }
    }
}