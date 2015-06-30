using ITI.ItSchool.Models;
using ITI.ItSchool.Models.ClassExercicesPlug;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
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

            //Here we will save data to the database
            if (ModelState.IsValid != false)
            {
                SQLRepository sUserRepo = new SQLRepository();
                var user = sUserRepo.FindByNickname(u.Nickname);
                if (user == null)
                {
                    sUserRepo.Create(u);
                    message = "Le compte a bien été créé.";
                }
                else
                    message = "Le pseudo existe déjà";
            }
            else
            {
                message = "Failed!";
            }
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetClasses()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetClasses();
            return jsonData;
        }

        public JsonResult GetClozeExercise(string exerciseName)
        {
            IRepository db = new SQLRepository();
            var exerciseData = db.GetClozeExerciseContent(exerciseName);
            return exerciseData;
        }

        public JsonResult GetGroups()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGroups();
            return jsonData;
        }

        public string CreateClozeExercise(ExerciseClozeData exerciseCloze)
        {
            IRepository db = new SQLRepository();
            string creationInfo = db.CreateExerciseCloze(exerciseCloze);
            return creationInfo;
        }

        public JsonResult GetChapters()
        {
            IRepository db = new SQLRepository();
            var chapters = db.GetChapters();
            return chapters;
        }
    }
}