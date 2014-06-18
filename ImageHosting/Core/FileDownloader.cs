using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using ImageHosting.Models;

namespace ImageHosting.Core
{
    public class FileDownloader
    {
        private readonly WebClient _client;
        private readonly IdToFileConverter _converter;

        public FileDownloader(IdToFileConverter converter)
        {
            _converter = converter;
            _client = new WebClient();
        }

        public void DownloadFile(StaticObject obj)
        {
            _client.DownloadFileAsync(new Uri(obj.Link),
                HttpContext.Current.Server.MapPath("~/App_Data/" + _converter.Convert(obj.Id) +
                                                   Path.GetExtension(obj.Link)));
        }
    }
}