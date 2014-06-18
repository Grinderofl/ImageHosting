using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageHosting.Controllers
{
    public class HomeController : Controller
    {
        public FileResult Index(string image)
        {
            var extension = "jpg";
            return File(Server.MapPath("~/App_Data/" + image), "image/" + extension);
        }

        public ActionResult Refresh()
        {
            
        }
    }
}