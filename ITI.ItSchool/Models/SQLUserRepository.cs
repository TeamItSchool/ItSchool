﻿using ITI.ItSchool.Models.UserEntities;
using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public class SQLUserRepository : IUserRepository
    {
        /// <summary>
        /// Creates a new User add it to the database.
        /// We have to take care / the user now is creating a GradeID and a RightID
        /// </summary>
        /// <param name="user">The user to create as an object</param>
        /// <returns>The new user who was created.</returns>
        public bool Create( User user )
        {
            if( user == null ) throw new ArgumentNullException( "The 'User' as an object type is null.", "user" );

            #region User Elements To Not Have Exception + affectation
            var c = new Clothe
            {
                Name = "TestClothe",
                Link = "TestLink",
                Remarks = "TestRemarks",
            };

            var e = new Eye
            {
                Name = "TestEye",
                Link = "TestLink",
            };

            var h = new Hair()
            {
                Name = "TestHair",
                Link = "TestLink"
            };

            var m = new Mouth
            {
                Name = "TestMouth",
                Link = "TestMouthLink"
            };

            var n = new Nose
            {
                Name = "TestNose",
                Link = "NoseLink"
            };

            var right = new Right
            {
                Name = "TestRight",
                Remarks = "TestRemarkRight"
            };

            var grade = new Grade
            {
                Name = "TestGrade",
                Remarks = "TestRemarkGrade"
            };

            var a = new Avatar
            {
                Name = "TestAvatar",
                Clothe = c,
                Eye = e,
                Hair = h,
                Mouth = m,
                Nose = n,
                Remarks = "Avatar Remarks"
            };

            var g = new Group
            {
                Name = "TestGroup",
                Remarks = "GroupRemarks..."
            };

            user.Avatar = a;
            user.Grade = grade;
            user.Right = right;
            user.Remarks = "This is a test...";
            #endregion

            using ( var userContext = new UserContext() )
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Finds a user by his nickname
        /// </summary>
        /// <param name="nickname">The concerned user's nickname.</param>
        /// <returns>a User</returns>
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
                User user = uc.Users.Where( a => a.Nickname.Equals( nickname ) ).FirstOrDefault();
                var jsonData = new JsonResult { Data = user, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                return jsonData;
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
                userToUpdate[0].Mail = user.Mail;

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
