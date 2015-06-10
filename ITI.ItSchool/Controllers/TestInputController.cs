using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITI.ItSchool.Controllers
{
    public class TestInputController : Controller
    {
        // GET: TestInput
        public ActionResult Index()
        {
            string sentence = "Je suis un petit garçon.";

            string[] words = sentence.Split( new char[] { ' ', ',', '.' } );

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals("suis"))
                {
                    ViewData["word"] = words[i];
                }
            }            
            return View( "Input" );
        }
    }
}