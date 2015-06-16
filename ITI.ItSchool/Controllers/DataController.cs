﻿using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
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

        public JsonResult SaveDictation( Game g )
        {
            string message = "";
            IRepository iRepo = new SQLRepository();
            string[] words = g.Data.Split( '/' );
            g.Data = words[1];
            using( GameContext gc = new GameContext() ) 
            {
                using( UserContext uc = new UserContext() )
                {
                    User user = iRepo.FindByNickname( words[0] );
                    g.Chapter = new Models.SchoolEntities.Chapter();
                    g.Chapter.GradeId = user.GradeId;
                    g.Chapter.Grade = null;
                }
                g.ExerciseTypeId = gc.ExerciseTypes.Where( e => e.Name.Equals( g.ExerciseType.Name ) ).Select(e=>e.ExerciseTypeId).FirstOrDefault();
                g.ExerciseType = null;
                g.LevelId = gc.Levels.Where( l => l.Name.Equals( g.Level.Name ) ).Select( l => l.LevelId ).FirstOrDefault();
                g.Level = null;
                g.Chapter.Name = "Dictée";
                using(SchoolContext sc = new SchoolContext()) 
                {
                    Chapter chap = sc.Chapters.Where( c => c.Name.Equals( "Dictée" ) ).FirstOrDefault();
                    g.ChapterId = chap.ChapterId;
                    g.Chapter = null;
                    g.Name = "Dictée" + sc.Grades.Where( gr => gr.GradeId.Equals( g.ChapterId ) ).Select( gr => gr.Name ).FirstOrDefault() + gc.Levels.Where( l => l.LevelId.Equals( g.LevelId ) ).Select( l => l.Name ).FirstOrDefault();
                }
                Game game = gc.Games.Where( mg => mg.Name.Equals( g.Name ) ).FirstOrDefault();
                if( game == null )
                {
                    gc.Games.Add( g );
                    gc.SaveChanges();
                    message = "Jeu enregistré";
                }
                else
                    message = "Un problème est survenu lors de l'enregistrement." + Environment.NewLine 
                        + "Veuillez réesayer.";      
                JsonResult data = new JsonResult { Data = message, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return data;
            }
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

        public void SaveClozeExercise( Game g )
        {
            IRepository iRepo = new SQLRepository();
            string[] words = g.Data.Split('/');
            g.Data = words[1];
            using (GameContext gc = new GameContext())
            {
                using (UserContext uc = new UserContext())
                {
                    User user = iRepo.FindByNickname(words[0]);
                    g.Chapter = new Models.SchoolEntities.Chapter();
                    g.Chapter.GradeId = user.GradeId;
                    g.Chapter.Grade = null;
                }

                g.ExerciseTypeId = gc.ExerciseTypes.Where(e => e.Name.Equals(g.ExerciseType.Name)).Select(e => e.ExerciseTypeId).FirstOrDefault();
                g.ExerciseType = null;
                g.LevelId = gc.Levels.Where(l => l.Name.Equals(g.Level.Name)).Select(l => l.LevelId).FirstOrDefault();
                g.Level = null;
                g.Chapter.Name = "Verbes à l'infinitif";
                using (SchoolContext sc = new SchoolContext())
                {
                    Chapter chap = sc.Chapters.Where(c => c.Name.Equals("Dictée")).FirstOrDefault();
                    g.ChapterId = chap.ChapterId;
                    g.Chapter = null;
                    g.Name = "Cloze_exercise" + sc.Grades.Where(gr => gr.GradeId.Equals(g.ChapterId)).Select(gr => gr.Name).FirstOrDefault() + gc.Levels.Where(l => l.LevelId.Equals(g.LevelId)).Select(l => l.Name).FirstOrDefault();
                }
                gc.Games.Add(g);
                gc.SaveChanges();
            }
        }

        public JsonResult GetGrades()
        {
            IRepository repo = new SQLRepository();
            var jsonData = repo.GetGrades();
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