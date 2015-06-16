using ITI.ItSchool.Models.AvatarEntities;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

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

        public bool Create( Class @class )
        {
            using( var db = new SchoolContext() )
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    db.Grades.Add(@class);
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
            Avatar userAvatar = new Avatar();
            Class grade = new Class();
            Group group = new Group();
            bool mailExists = true;
            bool emptyFields = false;
            if( user == null ) throw new ArgumentNullException( "The 'User' as an object type is null.", "user" );

            using ( var userContext = new UserContext() )
            {
                userContext.Configuration.LazyLoadingEnabled = false;
                userToCreate = userContext.Users.Where(u => u.Nickname.Equals( user.Nickname ) ).FirstOrDefault();
                mailExists = this.CheckExistingMail( user );
                emptyFields = this.CheckEmptyFields( user );

                if( userToCreate == null )
                {
                    if( !mailExists && !emptyFields )
                    {
                        using( var avatarContext = new AvatarContext() )
                        {
                            avatarContext.Configuration.LazyLoadingEnabled = false;
                            userAvatar.Name = "Avatar_" + user.Nickname;
                            Body body = avatarContext.Bodies.Where( b => b.Name.Equals( "Corps" ) ).FirstOrDefault();
                            userAvatar.Body = null;
                            userAvatar.BodyId = body.BodyId;
                            Foot feet = avatarContext.Feet.Where( f => f.Name.Equals( "Pieds" ) ).FirstOrDefault();
                            userAvatar.Feet = null;
                            userAvatar.FootId = feet.FootId;
                            Legs legs = avatarContext.Legs.Where( l => l.Name.Equals( "Jambes" ) ).FirstOrDefault();
                            userAvatar.Legs = null;
                            userAvatar.LegsId = legs.LegsId;
                            Avatar a = avatarContext.Avatars.OrderByDescending( av => av.AvatarId ).FirstOrDefault();

                            if( a == null )
                                user.AvatarId = 1;
                            else
                                user.AvatarId = a.AvatarId + 1;

                            user.Avatar = userAvatar;
                            userAvatar.User = user;
                            user.Avatar = userAvatar;
                        }
                        using( SchoolContext sc = new SchoolContext() )
                        {
                            sc.Configuration.LazyLoadingEnabled = false;
                            aClass = sc.Grades.Where( c => c.Name.Equals( user.Class.Name ) ).FirstOrDefault();
                            user.Class = null;
                            user.ClassId = aClass.ClassId;
                        }
                        User searchedUser = userContext.Users.OrderByDescending( u => u.UserId ).FirstOrDefault();

                        userAvatar.User = null;
                        if( searchedUser == null )
                            userAvatar.UserId = 1;
                        else
                            userAvatar.UserId = searchedUser.UserId+1;

                        group = userContext.Groups.Where( gr => gr.Name.Equals( user.Group.Name ) ).FirstOrDefault();
                        user.Group = null;
                        user.GroupId = group.GroupId;
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
                User user = uc.Users
                    /*.Include("Avatar")
                        .Include("Avatar.Body")
                        .Include("Avatar.Feet")
                        .Include("Avatar.Legs")
                    .Include("Group")
                    .Include("Grade")*/
                    .Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
                var jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
            }
        }

        public JsonResult SetExercise( Exercise exercise )
        {
            JsonResult jr = null;

            using ( var db = new ExerciseContext() )
            {
                try
                {
                    Exercise e = db.Exercises.Add( exercise );
                    db.SaveChanges();
                    var jsonData = new JsonResult { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public JsonResult GetGrades()
        {
            List<Class> grades = new List<Class>();
            using( var db = new SchoolContext() )
            {
                db.Configuration.LazyLoadingEnabled = false;
                grades = db.Grades.ToList();
                //grades = db.Grades.OrderBy( g => g.Name ).ToList();
                var jsonData = new JsonResult { Data = grades, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
            }
        }

        public JsonResult GetGroups()
        {
            List<Group> groups = new List<Group>();
            using (var db = new UserContext())
            {
                groups = db.Groups.OrderBy(g => g.Name).ToList();
                var jsonData = new JsonResult { Data = groups, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
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

        public Class aClass { get; set; }
    }
}