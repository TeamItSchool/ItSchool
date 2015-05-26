using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITI.ItSchool.Controllers
{
    public class DrawController : Controller
    {
        // GET: Draw
        public ActionResult Painting()
        {
            return View( "Painting" );
        }
    }
}