using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageHosting.Core;
using WebGrease.Css.Extensions;

namespace ImageHosting.Controllers
{
    public class HomeController : Controller
    {
        private EmailRetriever _retriever;

        public HomeController(EmailRetriever retriever)
        {
            _retriever = retriever;
        }

        public ActionResult Index(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return new HttpUnauthorizedResult();

            var url = id.Split('-');
            if (url.Length != 2) return new HttpNotFoundResult();
            var extension = "jpg";

            var path = string.Format("{0}\\{1}\\{2}", Server.MapPath("~/App_Data/"), url[0], url[1]);
            if (!System.IO.File.Exists(path)) return new HttpNotFoundResult();
            return File(path, "image/" + extension);
        }

        public string Refresh()
        {
            _retriever.Retrieve();
            return "Done";
        }

        public ActionResult List(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return new HttpNotFoundResult();
            var path = string.Format("{0}\\{1}", Server.MapPath("~/App_Data/"), id);
            if(!Directory.Exists(path)) return new HttpNotFoundResult();
            var files = Directory.GetFiles(path);
            var values = new List<string>();
            foreach (var file in files)
                values.Add(id + "-" + Path.GetFileName(file));
            return View(values.ToArray());
        }
    }
}