using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaceFighterWeb.Controllers
{
    public class GameController : Controller
    {
        public ActionResult Index()
        {
            Response.Redirect("https://spacefighterweb.azurewebsites.net/webgl");
            return Content("We're almost ready to let you play...");
        }
    }
}