using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageHosting.Core.Library;

namespace ImageHosting.Core
{
    public class IdToFileConverter
    {
        public string Convert(string id)
        {
            var date = DateTime.Now.ToString("O");
            return id + HttpServerUtility.UrlTokenEncode(date.GetBytes());
        }
    }
}