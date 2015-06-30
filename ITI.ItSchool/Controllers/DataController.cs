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
        public JsonResult UserLogin( LoginData d )
        {
            SQLRepository sUserRepo = new SQLRepository();
            var jsonData = sUserRepo.FindUserByNickname( d.Username );
            return jsonData;
        }

        public JsonResult SaveDragAndDropTeacher( CardsData c )
        {
            JsonResult jsonData = null;
            return jsonData;
        }

        public JsonResult SaveDictation( ExerciseDictationData dictationData )
        {
            IRepository repo = new SQLRepository();
            JsonResult messageData = repo.SaveDictation( dictationData );
            return messageData;
        }

        

        public JsonResult GetSpecificChilden( int id )
        {
            IRepository repo = new SQLRepository();
            JsonResult data = repo.GetChildrenByClassId( id );
            return data;
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

        public JsonResult SaveBattleCard(ExerciseBattleCard exoBattleCard)
        {
            string[] words = exoBattleCard.Choice.Split('/');
            string nickname = words[0];
            exoBattleCard.Choice = words[1];
            string message = "";

            using (var exoBattleCardContext = new ExerciseBattleCardContext())
            {
                using (var uc = new UserContext())
                {
                    IRepository repo = new SQLRepository();
                    User user = repo.FindByNickname(nickname);
                    exoBattleCard.Chapter = new Chapter();
                    exoBattleCard.Chapter.ClassId = user.ClassId;
                    exoBattleCard.Chapter.Class = null;
                }

                exoBattleCard.Chapter.Name = "Chapitre 1";

                using (var sc = new SchoolContext())
                {
                    Chapter chapter = sc.Chapters.Where(c => c.Name.Equals(exoBattleCard.Chapter.Name))
                                                 .FirstOrDefault();

                    exoBattleCard.ChapterId = chapter.ChapterId;
                    exoBattleCard.Name = "BattleCard " + sc.Classes
                                            .Where(cl => cl.ClassId.Equals(exoBattleCard.Chapter.ClassId))
                                            .Select(cl => cl.Name)
                                            .FirstOrDefault() + exoBattleCardContext.Level
                                            .Where(l => l.LevelId.Equals(exoBattleCard.LevelId))
                                            .Select(l => l.Name)
                                            .FirstOrDefault();
                    exoBattleCard.Chapter = null;
                    
                }
                ExerciseBattleCard battleCard = exoBattleCardContext.ExerciseBattleCard.Where(ebattleCard => ebattleCard.Name.Equals(exoBattleCard.Name) && ebattleCard.Level.Name.Equals(exoBattleCard.Level.Name)).FirstOrDefault();
                if (battleCard == null)
                {
                    // exoBattleCardContext.ExerciseBattleCard.Add(exoBattleCard);
                    //exoBattleCardContext.SaveChanges();
                    message = "Jeu enregistré";
                }
                else
                {
                    //battleCard.Choice = exoBattleCard.Choice;
                    //battleCard.Users = exoBattleCard.Users;
                    ////3. Mark entity as modified
                    //exoBattleCardContext.Entry(battleCard).State = System.Data.Entity.EntityState.Modified;

                    ////4. call SaveChanges
                    //exoBattleCardContext.SaveChanges();
                    message = "Choix mis à jour.";
                }
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }
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
        public JsonResult Register( User u )
        {
            string message = "";

            //Here we will save data to the database
            if( ModelState.IsValid != false )
            {
                SQLRepository sUserRepo = new SQLRepository();
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
            return new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetClasses()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetClasses();
            return jsonData;
        }

        public JsonResult GetClozeExercise()
        {
            throw new NotImplementedException();
        }

        public JsonResult GetGroups()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGroups();
            return jsonData;
        }

        public string CreateClozeExercise( ExerciseClozeData exerciseCloze )
        {
            IRepository db = new SQLRepository();
            string creationInfo = db.CreateExerciseCloze( exerciseCloze );
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