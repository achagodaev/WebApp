using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.Repositories
{
    public class SessionRepository
    {
        public static T GetSessionJson<T>(Controller controller, string key)
        {
            if (controller.Session[key] == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>((string)controller.Session[key]);
        }

        public static void SetSessionJson<T>(Controller controller, string key, object value)
        {
            controller.Session[key] = JsonConvert.SerializeObject(value);
        }
    }
}