using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.UI.Repositories
{
    public class TempDataRepository
    {
        public static T GetTempDataJson<T>(Controller controller, string key)
        {
            if (controller.TempData[key] == null)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>((string)controller.TempData[key]);
        }

        public static void SetTempDataJson<T>(Controller controller, string key, object value)
        {
            controller.TempData[key] = JsonConvert.SerializeObject(value);
        }
    }
}