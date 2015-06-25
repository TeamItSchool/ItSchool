﻿using ITI.ItSchool.Models.AvatarEntities;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public class SQLRepository : IRepository
    {
        /// <summary>
        /// Checks if the fields on the client side are well filled
        /// </summary>
        /// <param name="user">The user which contains the fields (name, class, nickname...)</param>
        /// <returns>True if all the specified fields are not empty.</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new class (of pupils).
        /// </summary>
        /// <param name="class">The class object to create.</param>
        /// <returns>True if it has well created.</returns>
        public bool Create( Class @class )
        {
            bool isCreated = false;
            using( var db = new SchoolContext() )
            {
                db.Configuration.LazyLoadingEnabled = false;
                db.Classes.Add( @class );
                db.SaveChanges();
                isCreated = true;
            }
            return isCreated;
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
            Class aClass = new Class();
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

                if (userToCreate == null)
                {
                    if (!mailExists && !emptyFields)
                    {
                        using (var avatarContext = new AvatarContext())
                        {
                            avatarContext.Configuration.LazyLoadingEnabled = false;
                            userAvatar.Name = "Avatar_" + user.Nickname;
                            Body body = avatarContext.Bodies.Where(b => b.Name.Equals("Corps")).FirstOrDefault();
                            userAvatar.Body = null;
                            userAvatar.BodyId = body.BodyId;
                            Foot feet = avatarContext.Feet.Where(f => f.Name.Equals("Pieds")).FirstOrDefault();
                            userAvatar.Feet = null;
                            userAvatar.FootId = feet.FootId;
                            Legs legs = avatarContext.Legs.Where(l => l.Name.Equals("Jambes")).FirstOrDefault();
                            userAvatar.Legs = null;
                            userAvatar.LegsId = legs.LegsId;
                            Avatar a = avatarContext.Avatars.OrderByDescending(av => av.AvatarId).FirstOrDefault();

                            if (a == null) user.AvatarId = 1;
                            else user.AvatarId = a.AvatarId + 1;

                            user.Avatar = userAvatar;
                            userAvatar.User = user;
                            user.Avatar = userAvatar;
                        }

                        using (SchoolContext sc = new SchoolContext())
                        {
                            sc.Configuration.LazyLoadingEnabled = false;
                            aClass = sc.Classes.Where(c => c.Name.Equals(user.Class.Name)).FirstOrDefault();
                            user.Class = null;
                            user.ClassId = aClass.ClassId;
                        }
                        User searchedUser = userContext.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
                        userAvatar.User = null;

                        if (searchedUser == null) userAvatar.UserId = 1;
                        else userAvatar.UserId = searchedUser.UserId + 1;

                        group = userContext.Groups.Where(gr => gr.Name.Equals(user.Group.Name)).FirstOrDefault();
                        user.Group = null;
                        user.GroupId = group.GroupId;
                        userContext.Users.Add(user);
                        userContext.SaveChanges();
                        int i = user.UserId;
                        group = userContext.Groups.Include("Users").Where( gr => gr.Name.Equals( user.Group.Name ) ).FirstOrDefault();
                        return true;
                    }
                    else return false;

                }
                else return false;
            }
        }

        /// <summary>
        /// Finds a user by his nickname
        /// </summary>
        /// <param name="nickname">The concerned user's nickname.</param>
        /// <returns>the User</returns>
        public User FindByNickname( string nickname )
        {
            User user = null;
            using( var uc = new UserContext() )
            {
                user = uc.Users.Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
            }
            return user;
        }

        /// <summary>
        /// Find a user by his nickname
        /// </summary>
        /// <param name="nickname">String which represent the concerd user's nickname</param>
        /// <returns>JSon Data for AngularJS</returns>
        public JsonResult FindUserByNickname( string nickname )
        {
            JsonResult jsonData = null;
            using( var uc = new UserContext() )
            {
                uc.Configuration.LazyLoadingEnabled = false;
                User user = uc.Users
                    .Include("Class")
                    .Include("Group")
                    .Include("Avatar")
                    .Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
                jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                if( user != null )
                {
                    user.Class.Users = null;
                    user.Group.Users = null;
                    user.Avatar.User = null;
                }
                else
                {
                    LoginData d = new LoginData();
                    d.Username = null;
                    d.Password = null;
                    jsonData = new JsonResult { Data = d, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            }
            return jsonData;
        }

        public JsonResult SetExercise( Exercise exercise )
        {
            JsonResult jr = null;

            using ( var db = new ExerciseContext() )
            {
                Exercise e = db.Exercises.Add( exercise );
                db.SaveChanges();
                var jsonData = new JsonResult { Data = e, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                jr = jsonData;
            }
            return jr;
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
            }

            return users;
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

                if (userToUpdate[0].Password.Equals(user.Password))
                {
                    userToUpdate[0].Nickname = user.Nickname;
                    userToUpdate[0].FirstName = user.FirstName;
                    userToUpdate[0].LastName = user.LastName;
                    userToUpdate[0].Mail = user.Mail;
                }
                else return null;

                return userToUpdate;
            }
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetChildrenByClassId( int id)
        {
            List<User> users = new List<User>();
            JsonResult data = null;
            using( UserContext uc = new UserContext() )
            {
                uc.Configuration.LazyLoadingEnabled = false;
                users = uc.Users
                    .Include("Avatar")
                    .Include("Class")
                    .Include("Group")
                    .Where( u => u.ClassId.Equals( id ) ).Where( u => u.Group.Name.Equals( "Élèves" ) ).ToList();

                for( int i = 0; i < users.Count(); i++ )
                {
                    users[i].Class.Users = null;
                    users[i].Group.Users = null;
                    users[i].Avatar.User = null;

                }
                data = new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }

        public List<User> GetChildrenListByClassId( int id )
        {
            List<User> children = new List<User>();
            using( UserContext uc = new UserContext() )
            {
                children = uc.Users.Where( u => u.ClassId.Equals( id ) ).Where( u => u.Group.Name.Equals( "Élèves" ) ).ToList();
            }
            return children;
        }

        public List<int> GetChildrenListIdByClassId( int id )
        {
            List<int> childrenIDs = new List<int>();
            using( UserContext uc = new UserContext() )
            {
                childrenIDs = uc.Users.Where( u => u.ClassId.Equals( id ) ).Where( u => u.Group.Name.Equals( "Élèves" ) ).Select( u => u.UserId ).ToList();
            }
            return childrenIDs;
        }

        public JsonResult GetClozeExerciseContent()
        {
            ExerciseCloze ec = new ExerciseCloze();
            JsonResult data = null;
            using( var db = new ExerciseClozeContext() )
            {
                db.Configuration.LazyLoadingEnabled = false;
                //var query = from e in db.ExerciseCloze
                //            where e.Name == "Mon premier texte à trous"
                //            select e;
                ec = db.ExerciseCloze.Where(e => e.Name.Equals("Mon premier texte à trous")).FirstOrDefault();
                data = new JsonResult { Data = ec, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }
       
        /// <summary>
        /// Gets all pupils' classes.
        /// </summary>
        /// <returns>All the classes in a list form in JSON Data format.</returns>
        public JsonResult GetClasses()
        {
            List<Class> classes = new List<Class>();
            JsonResult jsonData = null;
            using (var db = new SchoolContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                classes = db.Classes.ToList();
                jsonData = new JsonResult { Data = classes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return jsonData;
        }

        /// <summary>
        /// Gets all groups (Teacher, pupil,...).
        /// </summary>
        /// <returns>The groups as a JSON format.</returns>
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

        public JsonResult GetChapters()
        {
            List<Chapter> chapters = new List<Chapter>();
            using (var db = new SchoolContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                chapters = db.Chapters.ToList();
                return new JsonResult { Data = chapters, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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


        public JsonResult getBattleCardChoice()
        {
            using(var db = new ExerciseBattleCardContext())
            {
                //db.ExerciseBattleCard.Where(exBattle => exBattle.Choice.Equals())
            }
            throw new NotImplementedException();
        }


        public JsonResult getUsersByClasses(int id)
        {
            List<User> users = new List<User>();
            JsonResult data = null;
            using(var uc = new UserContext())
            {
                uc.Configuration.LazyLoadingEnabled = false;
                
                users = uc.Users
                    .Include("Avatar")
                    .Include("Class")
                    .Include("Group")
                    .Where(u => u.ClassId.Equals(id)).Where( u => u.Group.Name.Equals("Élèves") ).ToList();

                for (int i = 0; i < users.Count(); ++i)
                {
                    users[i].Class.Users = null;
                    users[i].Group.Users = null;
                    users[i].Avatar.User = null;
                }
                data =  new JsonResult { Data = users, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            return data;
        }
    }
}