using ITI.ItSchool.Models;
using ITI.ItSchool.Models.ClassExercicesPlug;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.PlugExercicesResults;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

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
        public JsonResult UserLogin(LoginData d)
        {
            SQLRepository sUserRepo = new SQLRepository();
            var jsonData = sUserRepo.FindUserByNickname(d.Username);
            return jsonData;
        }

        public JsonResult SaveDragAndDropTeacher(CardsData c)
        {
            JsonResult jsonData = null;
            return jsonData;
        }

        public JsonResult SaveDictation(ExerciseDictationData dictationData)
        {
            IRepository repo = new SQLRepository();
            JsonResult messageData = repo.SaveDictation(dictationData);
            return messageData;
        }

        public JsonResult GetSpecificChilden(int id)
        {
            IRepository repo = new SQLRepository();
            JsonResult data = repo.GetChildrenByClassId(id);
            return data;
        }

        public JsonResult GetDictation( int id )
        {
            IRepository repo = new SQLRepository();
            ExerciseDictation exoDictation = repo.FindExerciseDictationByLevelId( id );
            return new JsonResult { Data = exoDictation, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// Will search the exercise dictation in db from the user id and will define if a level is showable for the kid
        /// </summary>
        /// <returns></returns>
        public JsonResult GetExerciseDictation(int id)
        {
            List<ExerciseDictation> exercises = new List<ExerciseDictation>();
            List<ExerciseAffectation> childAffectations = new List<ExerciseAffectation>();
            List<int> exercisesIDs = new List<int>();

            IRepository repo = new SQLRepository();

            // Warning : It can't be null
            User concernedChild = repo.FindById(id);

            childAffectations = repo.GetExerciseAffectationListByUserId(concernedChild.UserId);

            for (int i = 0; i < childAffectations.Count(); i++) {
                ExerciseDictation dictation = repo.FindExerciseDictationById( childAffectations[i].ExerciseId );
                if(dictation != null)
                    exercisesIDs.Add( childAffectations[i].ExerciseId );
            }
            exercises = repo.GetExerciseDictationListById(exercisesIDs);

            JsonResult data = new JsonResult { Data = exercises, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return data;
        }

        public JsonResult CheckDictationText( ExerciseDictation sentExerciseDictation )
        {
            //Here we stock the user's nickname and the text data
            string[] words = sentExerciseDictation.Text.Split( '/' );

            //User Nickname is getted here
            string nickname = words[0];

            // Reaffecting correct data on the exercise
            sentExerciseDictation.Text = words[1];

            sentExerciseDictation.Text.Trim();
            int i = 0;
            bool success = false;
            List<string> wrongEntries = new List<string>();

            string message = "";

            IRepository repo = new SQLRepository();

            User user = repo.FindByNickname(nickname);

            ExerciseDictation originalExerciseDictation = repo.FindExerciseDictationById( sentExerciseDictation.ExerciseDictationId );

            string[] textPieces = sentExerciseDictation.Text.Split( new Char[] { ' ' } );
            string[] comparativeTextPieces = originalExerciseDictation.Text.Split( new Char[] { ' ' } );
            if (textPieces.Count() < comparativeTextPieces.Count())
                success = false;
            else if( textPieces.Count() > comparativeTextPieces.Count() )
                success = false;
            else
            {
                foreach( string tP in textPieces )
                {
                    if( tP != comparativeTextPieces[i] )
                        wrongEntries.Add( tP );
                    i++;
                }
                if( wrongEntries.Count == 0 )
                    success = true;
            }

            if( success == true )
                message = "Bravo, tu as parfaitement réussi la dictée !";
            else
                message = "La correction va être faite par ton professeur.";

            ExercisesResults exoResults = new ExercisesResults();
            ExerciseAffectation exoAffected = new ExerciseAffectation();
            using( ExerciseContext exoContext = new ExerciseContext() )
            {
                exoAffected = exoContext.ExercisesAffectations.Where( e => e.ExerciseId.Equals( sentExerciseDictation.ExerciseDictationId ) ).Where( e => e.UserId.Equals( user.UserId ) ).FirstOrDefault();
            }

            ExerciseDictationResults searchedExoDictationResult = new ExerciseDictationResults();
            using( ExerciseDictationResultsContext exoDictContext = new ExerciseDictationResultsContext() )
            {
                searchedExoDictationResult = exoDictContext.ExerciseDictationResults.Where( e => e.Name.Equals( sentExerciseDictation.Name + nickname ) ).FirstOrDefault();

            }

            if( searchedExoDictationResult == null )
            {
                exoResults.ExerciseResultsId = exoAffected.ExerciseAffectationId;
                using( ExerciseContext exoContext = new ExerciseContext() )
                {
                    exoContext.ExercisesResults.Add( exoResults );
                    exoContext.SaveChanges();
                }

                ExerciseDictationResults exoDictationResults = new ExerciseDictationResults();
                exoDictationResults.ExerciseDictationResultsId = exoResults.ExerciseResultsId;
                exoDictationResults.Name = sentExerciseDictation.Name + nickname;
                exoDictationResults.SubmittedText = sentExerciseDictation.Text;

                using( ExerciseDictationResultsContext exoDictationResultsContext = new ExerciseDictationResultsContext() )
                {
                    exoDictationResultsContext.ExerciseDictationResults.Add( exoDictationResults );
                    exoDictationResultsContext.SaveChanges();
                }
            }
            else
            {
                using( ExerciseDictationResultsContext exoDicResultsContext = new ExerciseDictationResultsContext() )
                {
                    searchedExoDictationResult.SubmittedText = sentExerciseDictation.Text;
                    searchedExoDictationResult.ExercisesResults = null;
                    //3. Mark entity as modified
                    exoDicResultsContext.Entry( searchedExoDictationResult ).State = System.Data.Entity.EntityState.Modified;
                    //4. call SaveChanges
                    exoDicResultsContext.SaveChanges();
                    if( success == true )
                        message = "Ton texte a été mis à jour et bravo, tu as parfaitement réussi la dictée !";
                    else
                        message = "Ton texte a bien été mis à jour. La correction va être faite par ton professeur.";
                }
            }

            // Warning, data is initialized to null
            JsonResult jsonData = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return jsonData;
        }

        public JsonResult SaveBattleCard(ExerciseBattleCardData exoBattleCardData)
        {
            IRepository repo = new SQLRepository();
            JsonResult messageData = repo.SaveBattleCard(exoBattleCardData);
            return messageData;
        }

        public JsonResult GetExerciseBattleCard(int id)
        {
            List<ExerciseBattleCard> exercises = new List<ExerciseBattleCard>();
            List<ExerciseAffectation> childAffectations = new List<ExerciseAffectation>();
            List<int> exercisesIDs = new List<int>();

            IRepository repo = new SQLRepository();

            // Warning : It can't be null
            User concernedChild = repo.FindById(id);

            childAffectations = repo.GetExerciseAffectationListByUserId(concernedChild.UserId);

            for (int i = 0; i < childAffectations.Count(); i++)
            {
                exercisesIDs.Add(childAffectations[i].ExerciseId);
            }

            exercises = repo.GetExerciseBattleCardListById(exercisesIDs);

            JsonResult data = new JsonResult { Data = exercises, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return data;
        }

        public JsonResult GetUsersByClasses(int id)
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.getUsersByClasses(id);
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
            string activationCode = Guid.NewGuid().ToString();

            try
            {
                MailMessage mail = new MailMessage();
                //mail.To.Add( email );
                mail.To.Add( u.Mail );

                //mail to the subscriber !
                mail.From = new MailAddress( "itschool.management.team@gmail.com" );


                mail.Subject = "Inscription sur It'School";

                string body = "Bonjour " + u.FirstName.Trim() + ",";
                body += "<br /><br />Merci de vous être inscrit sur le site itschool.edu.fr";
                body += "<br /><br />Afin de finaliser l'inscription, nous vous prions de cliquer sur le lien ci-dessous qui activera votre compte.";

                //Here is the URL to validate the registration
                //Will give access to a page wich validate the activation number
                
                body += "<br /><a href = 'http://localhost:18264/Data/CheckUrl?ActivationCode=" + activationCode + "'>Cliquez ici afin d'activer le compte.</a>";
                
                body += "<br /><br />";
                body += "<br /><br />Cordialement,";
                body += "<br /><br />L'équipe It'School.";

                mail.Body = body;

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                smtp.Credentials = new System.Net.NetworkCredential
                     ( "itschool.management.team@gmail.com", "LearnWithFun" ); // ***use valid credentials***
                smtp.Port = 587;

                //Or your Smtp Email ID and Password
                smtp.EnableSsl = true;
                smtp.Send( mail );
                message = "Un mail vous a été envoyé afin de confirmer votre inscription.";
            }
            catch( Exception ex )
            {
                message = "Exception in sendEmail : " + ex.Message;
            }

            //using( MailMessage mm = new MailMessage( "kikabouguenole@gmail.com", "kikabouguenole@gmail.com") )
            //{
            //    mm.Subject = "Account Activation";
            //    string body = "Hello " + u.FirstName.Trim() + ",";
            //    body += "<br /><br />Please click the following link to activate your account";
            //    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace( "CS.aspx", "CS_Activation.aspx?ActivationCode=" + activationCode ) + "'>Click here to activate your account.</a>";
            //    body += "<br /><br />Thanks";
            //    mm.Body = body;
            //    mm.IsBodyHtml = true;
            //    SmtpClient smtp = new SmtpClient();
            //    smtp.Host = "smtp.gmail.com";
            //    smtp.EnableSsl = true;
            //    NetworkCredential NetworkCred = new NetworkCredential( "kikabouguenole@gmail.com", "LesSimpsons:D" );
            //    smtp.UseDefaultCredentials = true;
            //    smtp.Credentials = NetworkCred;
            //    smtp.Port = 587;
            //    smtp.Send( mm );
            //}

            ////Here we will save data to the database
            //if (ModelState.IsValid != false)
            //{
            //    SQLRepository sUserRepo = new SQLRepository();
            //    var user = sUserRepo.FindByNickname(u.Nickname);
            //    if (user == null)
            //    {
            //        sUserRepo.Create(u);
            //        message = "Le compte a bien été créé.";
            //    }
            //    else
            //        message = "Le pseudo existe déjà";
            //}
            //else
            //{
            //    message = "Failed!";
            //}
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult CheckURL(string activationCode)
        {
            ViewBag.ActivationCode = activationCode;
            return View("CheckUrlView");
        }

        public JsonResult GetClasses()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetClasses();
            return jsonData;
        }

        public JsonResult GetClozeExercise( string exerciseName )
        {
            JsonResult jsonData = null;
            IRepository db = new SQLRepository();
            jsonData = db.GetClozeExerciseContent( exerciseName );
            return jsonData;
        }

        public JsonResult GetGroups()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGroups();
            return jsonData;
        }

        public string CreateClozeExercise(ExerciseClozeData exerciseCloze)
        {
            if (ModelState.IsValid)
            {
                IRepository db = new SQLRepository();
                string creationInfo = db.CreateExerciseCloze(exerciseCloze);
                return creationInfo;
            }
            else return "error validation form";
        }

        public string UpdateClozeExercise( ExerciseClozeData exerciseCloze )
        {
            throw new NotImplementedException();
        }

        public JsonResult GetChapters()
        {   
            IRepository db = new SQLRepository();
            var chapters = db.GetChapters();
            return chapters;
        }

        public JsonResult GetLevels()
        {
            IRepository db = new SQLRepository();
            var levels = db.GetLevels();
            return levels;
        }

        public JsonResult GetClozeExercises()
        {
            IRepository db = new SQLRepository();
            var jsonData = db.GetClozeExercises();
            return jsonData;
        }
    }
}