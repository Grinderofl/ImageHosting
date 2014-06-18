using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using ImageHosting.Models;

namespace ImageHosting.Core
{
    public class FileDownloader
    {
        private readonly WebClient _client;
        private readonly IdConverter _converter;

        public FileDownloader(IdConverter converter)
        {
            _converter = converter;
            _client = new WebClient();
        }

        public void DownloadFile(StaticObject obj)
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/" + _converter.ConvertLocation(obj.Id));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                var info = new DirectoryInfo(path);
                var security = info.GetAccessControl();
                security.AddAccessRule(new FileSystemAccessRule("images.nerosule.net", FileSystemRights.FullControl,
                    InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow));
                security.AddAccessRule(new FileSystemAccessRule("images.nerosule.net", FileSystemRights.FullControl,
                    InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                info.SetAccessControl(security);
            }
            var extension = Path.GetExtension(obj.Link);
            if (string.IsNullOrWhiteSpace(extension))
                extension = ".jpg";
            var file = path + "\\" + _converter.ConvertFileName(obj.Id) + extension;
            _client.DownloadFile(new Uri(obj.Link), file);
        }
    }
}