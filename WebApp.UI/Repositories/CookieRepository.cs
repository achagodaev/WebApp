using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.Repositories
{
    public class CookieRepository
    {
        public static T GetCookiesJson<T>(Controller controller, string key)
        {
            if (controller.HttpContext.Request.Cookies[key] == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(controller.HttpContext.Request.Cookies[key].Value);
        }

        public static void SetCookiesJson<T>(Controller controller, string key, object value)
        {
            controller.HttpContext.Response.Cookies[key].Value = JsonConvert.SerializeObject(value);
        }
    }
}