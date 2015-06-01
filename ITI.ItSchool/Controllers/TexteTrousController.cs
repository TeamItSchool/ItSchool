using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITI.ItSchool.Controllers
{
    public class TexteTrousController : Controller
    {
        // GET: TexteTrous
        public ActionResult Index()
        {
            return View( "ClozeExercise" );
        }
    }
}