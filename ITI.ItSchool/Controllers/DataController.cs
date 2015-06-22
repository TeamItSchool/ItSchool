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

        public JsonResult SaveDictation( ExerciseDictation ed )
        {
            string[] words = ed.Text.Split( '/' );
            string nickname = words[ 0 ];
            ed.Text = words[ 1 ];
            string message = "";

            using( var edc = new ExerciseDictationContext() )
            {
                using( var uc = new UserContext() )
                {
                    IRepository repo = new SQLRepository();
                    User user = repo.FindByNickname( nickname );
                    ed.Chapter = new Chapter();
                    ed.Chapter.ClassId = user.ClassId;
                    ed.Chapter.Class = null;
                }

                ed.ExerciseTypeId = edc.ExerciseType.Where( e => e.Name.Equals( ed.ExerciseType.Name ) )
                                                    .Select( e => e.ExerciseTypeId )
                                                    .FirstOrDefault();
                ed.ExerciseType = null;
                ed.Chapter.Name = "Dictée";

                using( var sc = new SchoolContext() )
                {
                    Chapter chapter = sc.Chapters.Where( c => c.Name.Equals( "Dictée" ) )
                                                 .FirstOrDefault();

                    ed.ChapterId = chapter.ChapterId;
                    ed.Chapter = null;
                    ed.Name = "Dictée " + sc.Classes
                                            .Where(cl => cl.ClassId.Equals(ed.ChapterId))
                                            .Select(cl => cl.Name)
                                            .FirstOrDefault() + edc.Level
                                            .Where(l => l.LevelId.Equals(ed.LevelId))
                                            .Select(l => l.Name)
                                            .FirstOrDefault();
                }
                ExerciseDictation dictation = edc.ExerciseDictation.Where(edictation => edictation.Name.Equals(ed.Name)).FirstOrDefault();
                if (dictation ==  null) {
                    edc.ExerciseDictation.Add( ed );
                    edc.SaveChanges();
                    message = "Jeu enregistré";
                }
                else {
                    dictation.Text = ed.Text;
                    //3. Mark entity as modified
                    edc.Entry( dictation ).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    edc.SaveChanges();
                    message = "Texte mis à jour.";
                }
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }

            //IRepository iRepo = new SQLRepository();
            //string[] words = g.Data.Split( '/' );
            //g.Data = words[1];
            //using( ExerciseContext gc = new ExerciseContext() ) 
            //{
            //    using( UserContext uc = new UserContext() )
            //    {
            //        User user = iRepo.FindByNickname( words[0] );
            //        g.Chapter = new Models.SchoolEntities.Chapter();
            //        g.Chapter.GradeId = user.GradeId;
            //        g.Chapter.Grade = null;
            //    }
            //    g.ExerciseTypeId = gc.ExerciseTypes.Where( e => e.Name.Equals( g.ExerciseType.Name ) ).Select(e=>e.ExerciseTypeId).FirstOrDefault();
            //    g.ExerciseType = null;
            //    g.LevelId = gc.Levels.Where( l => l.Name.Equals( g.Level.Name ) ).Select( l => l.LevelId ).FirstOrDefault();
            //    g.Level = null;
            //    g.Chapter.Name = "Dictée";
            //    using(SchoolContext sc = new SchoolContext()) 
            //    {
            //        /*g.Chapter.Theme = new Models.SchoolEntities.Theme();
            //        g.Chapter.Theme.Name = "Verbes irréguliers";
            //        Matter matter = sc.Matters.Where( m => m.Name.Equals( "Français" ) ).FirstOrDefault();
            //        g.Chapter.Theme.MatterId = matter.MatterId;*/

            //        Chapter chap = sc.Chapters.Where( c => c.Name.Equals( "Dictée" ) ).FirstOrDefault();
            //        g.ChapterId = chap.ChapterId;
            //        g.Chapter = null;
            //        g.Name = "Dictée" + sc.Grades.Where( gr => gr._ClassId.Equals( g.ChapterId ) )
            //                                              .Select( gr => gr.Name ).FirstOrDefault() + gc.Levels.Where( l => l.LevelId.Equals( g.LevelId ) ).Select( l => l.Name ).FirstOrDefault();
            //    }
            //    gc.Exercises.Add( g );
            //    gc.SaveChanges();
            //}
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

                exoBattleCard.ExerciseTypeId = exoBattleCardContext.ExerciseType.Where(e => e.Name.Equals(exoBattleCard.ExerciseType.Name))
                                                    .Select(e => e.ExerciseTypeId)
                                                    .FirstOrDefault();
                exoBattleCard.ExerciseType = null;
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
                ExerciseBattleCard battleCard = exoBattleCardContext.ExerciseBattleCard.Where(ebattleCard => ebattleCard.Name.Equals(exoBattleCard.Name)).FirstOrDefault();
                if (battleCard == null)
                {
                    exoBattleCardContext.ExerciseBattleCard.Add(exoBattleCard);
                    exoBattleCardContext.SaveChanges();
                    message = "Jeu enregistré";
                }
                else
                {
                    battleCard.Choice = exoBattleCard.Choice;
                    //3. Mark entity as modified
                    exoBattleCardContext.Entry(battleCard).State = System.Data.Entity.EntityState.Modified;

                    //4. call SaveChanges
                    exoBattleCardContext.SaveChanges();
                    message = "Choix mis à jour.";
                }
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }
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

        public void SaveClozeExercise( Exercise exercise )
        {
            throw new NotImplementedException();
        }

        public JsonResult GetClasses()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetClasses();
            return jsonData;
        }

        public JsonResult GetGroups()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGroups();
            return jsonData;
        }
    }
}