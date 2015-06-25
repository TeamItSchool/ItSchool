using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
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

        public JsonResult SaveDictation( ExerciseDictation dictationExo )
        {

            string[] words = dictationExo.Text.Split( '/' );
            string nickname = words[0];
            dictationExo.Text = words[1];
            string message = "";

            IRepository repo = new SQLRepository();
            User user = repo.FindByNickname( nickname );

            using( var edc = new ExerciseDictationContext() )
            {
                dictationExo.Chapter = new Chapter();
                dictationExo.Chapter.ClassId = user.ClassId;
                dictationExo.Chapter.Class = null;
                dictationExo.Chapter.Name = "Dictée";

                using( var sc = new SchoolContext() )
                {
                    Chapter chapter = sc.Chapters.Where( c => c.Name.Equals( "Dictée" ) )
                                                 .FirstOrDefault();

                    dictationExo.ChapterId = chapter.ChapterId;
                    dictationExo.LevelId = edc.Level.Where( l => l.Name.Equals( dictationExo.Level.Name ) ).Select( l => l.LevelId ).FirstOrDefault();
                    dictationExo.Level = null;
                    dictationExo.Name = "Dictée " + sc.Classes
                                            .Where( cl => cl.ClassId.Equals( dictationExo.Chapter.ClassId ) )
                                            .Select( cl => cl.Name )
                                            .FirstOrDefault() + edc.Level
                                            .Where( l => l.LevelId.Equals( dictationExo.LevelId ) )
                                            .Select( l => l.Name )
                                            .FirstOrDefault();
                    dictationExo.Chapter = null;
                }
                ExerciseDictation dictation = edc.ExerciseDictation.Where( exDictation => exDictation.Name.Equals( dictationExo.Name ) ).FirstOrDefault();
                if( dictation == null )
                {
                    if( dictationExo.LevelId.Equals( 1 ) )
                        //dictationExo.Users = repo.GetChildrenListByClassId( user.ClassId );

                    edc.ExerciseDictation.Add( dictationExo );
                    edc.SaveChanges();
                    message = "Jeu enregistré";
                }
                else
                {
                    ExerciseDictation refExoDic = new ExerciseDictation();
                    using( ExerciseDictationContext exoDictationContext = new ExerciseDictationContext() )
                    {
                        refExoDic = exoDictationContext.ExerciseDictation.Where( ex => ex.Name.Equals( dictationExo.Name ) ).FirstOrDefault();
                    }
                    dictation.Text = dictationExo.Text;

                    dictation.AudioData = dictationExo.AudioData;
                    //3. Mark entity as modified
                    edc.Entry( dictation ).State = System.Data.Entity.EntityState.Modified;
                    //4. call SaveChanges
                    edc.SaveChanges();
                    message = "Texte mis à jour.";
                }
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }
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
                    //exoBattleCardContext.ExerciseBattleCard.Add(exoBattleCard);
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
            IRepository db = new SQLRepository();
            var exerciseData = db.GetClozeExerciseContent();
            return exerciseData;
        }

        public JsonResult GetGroups()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGroups();
            return jsonData;
        }

        public void SaveClozeExercise(ExerciseCloze exerciseCloze)
        {

        }

        public JsonResult GetChapters()
        {
            IRepository db = new SQLRepository();
            var chapters = db.GetChapters();
            return chapters;
        }
    }
}