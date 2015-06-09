using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.UserEntities;
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

        /// <summary>
        /// Login with client side AngularJS infos
        /// </summary>
        /// <param name="d">LoginData Object (contains Username + Password from method POST)</param>
        /// <returns>JSon Data for AngularJS informations</returns>
        public JsonResult UserLogin( LoginData d )
        {
            SQLUserRepository sUserRepo = new SQLUserRepository();
            var jsonData = sUserRepo.FindUserByNickname( d.Username );
            return jsonData;

            #region Code For Login With TestDBEntities
            /*
            using( TestDBEntities dc = new TestDBEntities() )
            {
                var user = dc.User1.Where( a => a.UserName.Equals( d.Username ) && a.Password.Equals( d.Password ) ).FirstOrDefault();
                var jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
            }*/
            #endregion
        }

        public JsonResult SaveDragAndDropTeacher( CardsData c )
        {
            JsonResult jsonData = null;
            return jsonData;
        }

        public JsonResult SaveDictation( DictationText d )
        {
            d.Text.Trim();
            DictationText text = new DictationText();
            text.Text = d.Text;
            text.Level = d.Level;
            JsonResult jsonData = new JsonResult { Data = text, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return jsonData;
        }

        public JsonResult CheckDictationText( DictationText d )
        {
            d.Text.Trim();
            int i = 0;
            bool success = false;
            List<string> wrongEntries = new List<string>();
            DictationText text = new DictationText();
            text.Text = "Je suis celui qui me trouve dans la fôret. La fôret est grande, magnifique. Je suis heureux de me trouver dans cette forêt";
            text.Level = d.Level;
            string[] textPieces = d.Text.Split( new Char[] { ' ' } );
            string[] comparativeTextPieces = text.Text.Split( new Char[] { ' ' } );
            if( textPieces.Count() < comparativeTextPieces.Count() )
                success = false;
            foreach( string tP in textPieces )
            {
                if( tP != comparativeTextPieces[i] )
                    wrongEntries.Add( tP );
            }
            if( wrongEntries.Count == 0 )
                success = true;

            JsonResult jsonData = new JsonResult { Data = text, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return jsonData;
        }

        /// <summary>
        /// Save User in DB
        /// </summary>
        /// <param name="u">Object User Sent From AngularJS</param>
        /// <returns>JSon Data For AngularJS</returns>
        [HttpPost]
        public JsonResult Register(User u)
        {
            string message = "";

            //Here we will save data to the database
            if( ModelState.IsValid != false )
            {
                SQLUserRepository sUserRepo = new SQLUserRepository();
                var user = sUserRepo.FindByNickname( u.Nickname );
                if( user == null )
                {
                    sUserRepo.Create( u );
                    message = "Le compte a bien été créé.";
                }
                else
                    message = "Le pseudo existe déjà";
            }
            else
            {
                message = "Failed!";
            }

            #region with TestDBEntites
            ////Here we will save data to the database
            //if( ModelState.IsValid == false)
            //{
            //    using (TestDBEntities dc = new TestDBEntities() )
            //    {
            //        // check if the username is available
            //        var user = dc.User1.Where( a => a.UserName.Equals( u.UserName ) ).FirstOrDefault();
            //        if( user == null )
            //        {
            //            //Save here
            //            u.UserID = dc.User1.Count();
            //            dc.User1.Add( u );
            //            dc.SaveChanges();
            //            if( u.Status == "2" )
            //                message = "Votre compte est bien créé.";
            //            else if( u.Status == "3" )
            //                message = "Ton compte a bien été créé.";
            //        }
            //        else
            //        {
            //            if( u.Status == "2" )
            //                message = "Ce pseudo existe déjà. Veuillez en choisir un autre.";
            //            else if( u.Status == "3" )
            //                message = "Oops, quelqu'un a déjà choisis ce pseudo. Essayes-en un autre.";
            //            else
            //                message = "Aïe..";
            //        }
            //    }
            //}
            //else
            //{
            //    message = "Failed!";
            //}
            #endregion
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}