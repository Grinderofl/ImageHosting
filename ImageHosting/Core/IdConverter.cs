using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageHosting.Core.Library;

namespace ImageHosting.Core
{
    public class IdConverter
    {
        public string ConvertFileName(string id)
        {
            var date = DateTime.Now.ToString("yyyyMMddHHmmss");
            return HttpServerUtility.UrlTokenEncode(date.GetBytes());
        }

        public string ConvertLocation(string id)
        {
            return id;
        }
    }
}