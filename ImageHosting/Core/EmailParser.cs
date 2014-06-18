﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ImageHosting.Models;
using Newtonsoft.Json;
using OpenPop.Mime;

namespace ImageHosting.Core
{
    public class EmailParser
    {
        private readonly FileDownloader _downloader;

        public EmailParser(FileDownloader downloader)
        {
            _downloader = downloader;
        }

        public void Parse(List<Message> messages)
        {
            foreach (var message in messages)
            {
                var text = message.FindFirstPlainTextVersion();
                if (text != null)
                {
                    var result = JsonConvert.DeserializeObject<StaticObject>(text.GetBodyAsText());
                    if (result != null)
                        _downloader.DownloadFile(result);
                }
            }
        }
    }
}