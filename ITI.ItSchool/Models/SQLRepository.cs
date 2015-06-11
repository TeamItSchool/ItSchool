using ITI.ItSchool.Models.UserEntities;
using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ITI.ItSchool.Models.SchoolEntities;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ITI.ItSchool.Models
{
    public class SQLRepository : IRepository
    {

        private bool CheckEmptyFields( User user )
        {
            if( String.IsNullOrEmpty( user.FirstName ) || String.IsNullOrEmpty( user.LastName ) ||
                String.IsNullOrEmpty( user.Mail ) || String.IsNullOrEmpty( user.Nickname ) ||
                String.IsNullOrEmpty( user.Password ) )
            {
                return true;
            }
            return false;
        }

        private bool CheckExistingMail( User user )
        {
            User usersMail = null;

            using( var db = new UserContext() )
            {
                usersMail = db.Users.Where( u => u.Mail.Equals( user.Mail ) ).FirstOrDefault();

                if( usersMail == null ) return false;
       
                return true;
            }
        }

        public bool Create( Game game )
        {
            if ( game.Equals( null ) ) throw new ArgumentNullException( "Game as a type is null", "game" );
            if ( game.Data.Equals( null ) ) throw new ArgumentNullException( "Game's data is null", "game" );

            using( var db = new GameContext() )
            {
                try
                {
                    db.Games.Add(game);
                    db.SaveChanges();
                    return true;
                }
                catch( Exception ex )
                {
                    throw;
                }
            }
        }

        public bool Create( Grade grade )
        {
            using( var db = new SchoolContext() )
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Grades.Add(grade);
                    db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Creates a new User add it to the database.
        /// We have to take care / the user now is creating a GradeID and a RightID
        /// </summary>
        /// <param name="user">The user to create as an object</param>
        /// <returns>True if the user was well created.</returns>
        public bool Create( User user )
        {
            User userToCreate = null;
            bool mailExists = true;
            bool emptyFields = false;
            if( user == null ) throw new ArgumentNullException( "The 'User' as an object type is null.", "user" );

            using ( var userContext = new UserContext() )
            {
                userToCreate = userContext.Users.Where(u => u.Nickname.Equals( user.Nickname ) ).FirstOrDefault();
                mailExists = this.CheckExistingMail( user );
                emptyFields = this.CheckEmptyFields( user );

                if( userToCreate == null )
                {
                    if( !mailExists && !emptyFields )
                    {
                        userContext.Users.Add(user);
                        userContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Finds a user by his nickname
        /// </summary>
        /// <param name="nickname">The concerned user's nickname.</param>
        /// <returns>the User</returns>
        public User FindByNickname( string nickname )
        {
            using( var uc = new UserContext() )
            {
                User user = uc.Users.Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
                return user;
            }
        }

        /// <summary>
        /// Find a user by his nickname
        /// </summary>
        /// <param name="nickname">String which represent the concerd user's nickname</param>
        /// <returns>JSon Data for AngularJS</returns>
        public JsonResult FindUserByNickname( string nickname )
        {
            using( var uc = new UserContext() )
            {
                uc.Configuration.LazyLoadingEnabled = false;
                User user = uc.Users.Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
                var jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
            }
        }

        public JsonResult SetExercise( Game game )
        {
            JsonResult jr = null;

            #region TestExerciseCreation
            game.Name = "Cloze";
            game.ChapterId = 1;
            //game.LevelId = 1;
            game.ExerciseTypeId = 1;
            //game.Remarks = "Testing creation";
            #endregion


            using ( var db = new GameContext() )
            {
                try
                {
                    Game g = db.Games.Add( game );
                    db.SaveChanges();
                    var jsonData = new JsonResult { Data = g, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    jr = jsonData;
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
                return jr;
            }
        }

        /// <summary>
        /// Finds all users in our DB
        /// </summary>
        /// <returns>A list of Users</returns>
        public IList<User> FindAllUsers()
        {
            IList<User> users = new List<User>();
            using( UserContext userContext = new UserContext() )
            {
                users = userContext.Users.ToList();
                return users;
            }
        }

        public IList<User> Update( User user )
        {
            IList<User> userToUpdate = new List<User>();
            using( var db = new UserContext() )
            {
                var query = from u in db.Users
                            where u.UserId == user.UserId
                            select u;

                userToUpdate = db.Users.ToList();

                if( userToUpdate[ 0 ].Password.Equals( user.Password ) )
                {
                    userToUpdate[ 0 ].Nickname = user.Nickname;
                    userToUpdate[ 0 ].FirstName = user.FirstName;
                    userToUpdate[ 0 ].LastName = user.LastName;
                    userToUpdate[ 0 ].Mail = user.Mail;
                }
                else
                {
                    return null;
                }

                return userToUpdate;
            }
        }

        public User FindById( int id )
        {
            throw new NotImplementedException();
        }

        public User FindByGrade( string grade )
        {
            throw new NotImplementedException();
        }

        public User FindByMail( string mail )
        {
            throw new NotImplementedException();
        }

        public bool Remove( int id )
        {
            throw new NotImplementedException();
        }

        public bool Remove( User u )
        {
            throw new NotImplementedException();
        }
    }
}