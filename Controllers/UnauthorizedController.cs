using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HGI.Controllers
{
    public class UnauthorizedController : Controller
    {
        //
        // GET: /Unauthorized/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error500()
        {
            return View();
        }

        public ActionResult Error403()
        {
            return View();
        }
    }
}
