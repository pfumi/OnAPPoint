using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnAPPoint.Util
{
  public class JsonUtil
  {
    private static JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    public static string seralizeObject(object value)
    {
      return JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented, DefaultJsonSerializerSettings);
    }

  }
}