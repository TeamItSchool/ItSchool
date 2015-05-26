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
            ViewData["Error"] = "";
            string theCase = null;

            if( coll != null && coll.Count == 2)
            {
                ViewData["Pseudo"] = coll[0];
                ViewData["Password"] = coll[1];
                if( String.IsNullOrWhiteSpace( ViewData["Pseudo"].ToString() ) || String.IsNullOrWhiteSpace( ViewData["Password"].ToString() )) {
                    theCase = "~/Views/Home/Index.cshtml";
                    ViewData["Error"] = "Veuillez entrer votre pseudo et votre mot de passe.";
                }
                else
                    theCase = "Index";
            }
            else if (coll.Count != 2)
            {
                ViewData["Error"] = "Attention vous n'avez pas entré de pseudo ou de mot de passe.";
                theCase = "~/Views/Home/Index.cshtml";
            }
            return View(theCase);
        }
    }
}