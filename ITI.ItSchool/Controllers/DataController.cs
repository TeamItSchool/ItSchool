using ITI.ItSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITI.ItSchool.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public JsonResult UserLogin( LoginData d )
        {
            using( TestDBEntities dc = new TestDBEntities() )
            {
                var user = dc.User1.Where( a => a.UserName.Equals( d.Username ) && a.Password.Equals( d.Password ) ).FirstOrDefault();
                return new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        //Save Simple Registration Form
        [HttpPost]
        public JsonResult Register(User1 u)
        {
             string message = "";
            //Here we will save data to the database
            if( ModelState.IsValid == false)
            {
                using (TestDBEntities dc = new TestDBEntities() )
                {
                    // check if the username is available
                    var user = dc.User1.Where( a => a.UserName.Equals( u.UserName ) ).FirstOrDefault();
                    if( user == null )
                    {
                        //Save here
                        u.UserID = dc.User1.Count();
                        dc.User1.Add( u );
                        dc.SaveChanges();
                        if( u.Status == "2" )
                            message = "Votre compte est bien créé.";
                        else if( u.Status == "3" )
                            message = "Ton compte a bien été créé.";
                    }
                    else
                    {
                        if( u.Status == "2" )
                            message = "Ce pseudo existe déjà. Veuillez en choisir un autre.";
                        else if( u.Status == "3" )
                            message = "Oops, quelqu'un a déjà choisis ce pseudo. Essayes-en un autre.";
                        else
                            message = "Aïe..";
                    }
                }
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}