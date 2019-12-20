using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kora_Today.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ControlController : Controller
    {
        //
        // GET: /Control/

        public ActionResult Index()
        {

            return View();
        }

    }
}
