using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITI.ItSchool.Controllers
{
    public class BackOfficeController : Controller
    {
        // GET: BackOffice
        public ActionResult Index()
        {
            NameValueCollection coll;
            coll = Request.Form;
            ViewData["Pseudo"] = coll[0];
            ViewData["Password"] = coll[1];
            if( String.IsNullOrWhiteSpace( ViewData["Pseudo"].ToString() ) || String.IsNullOrWhiteSpace( ViewData["Password"].ToString() ) )
                return View( "~/Views/Home/Index.cshtml" );
            else
                return View( "Index" );
        }
    }
}