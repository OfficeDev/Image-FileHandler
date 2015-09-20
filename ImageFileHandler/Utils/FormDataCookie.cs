using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web;
using System.Web.Script.Serialization;

namespace ImageFileHandler.Utils
{
    public class FormDataCookie
    {
        public string Name { get; private set; }
        
        public NameValueCollection FormData { get; private set; }

        public bool IsLoaded
        {
            get
            {
                return FormData != null;
            }
        }

        public FormDataCookie(string name)
            : this(name, true)
        {

        }
        
        public FormDataCookie(string name, bool tryLoad)
        {
            this.Name = name;
            this.FormData = null;
        }

        public bool Load()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[this.Name];

            if (cookie != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.RegisterConverters(new JavaScriptConverter[] { new NameValueCollectionConverter() });
                this.FormData = serializer.Deserialize<NameValueCollection>(cookie.Value);

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveRequestFormToCookie()
        {
            HttpCookie cookie = new HttpCookie(this.Name);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] { new NameValueCollectionConverter() });
            
            cookie.Value = serializer.Serialize(HttpContext.Current.Request.Form);
            cookie.Expires = DateTime.Now.AddMinutes(10);
            
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }

        public bool Clear()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[this.Name];
            
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private class NameValueCollectionConverter : JavaScriptConverter
        {
            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new Type[] { typeof(NameValueCollection) })); }
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                NameValueCollection collection = obj as NameValueCollection;
                Dictionary<string, object> result = new Dictionary<string, object>();

                if (collection != null)
                {
                    

                    foreach (string key in collection.AllKeys)
                    {
                        result.Add(key, collection[key]);
                    }
                }

                return result;
            }

            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                if (dictionary == null)
                    throw new ArgumentNullException("dictionary");

                if (type == typeof(NameValueCollection))
                {
                    NameValueCollection collection = new NameValueCollection();

                    foreach (string key in dictionary.Keys)
                    {
                        collection.Add(key, dictionary[key] as string);
                    }

                    return collection;
                }
                else
                {
                    return null;
                }
            }        
        }
    }
}