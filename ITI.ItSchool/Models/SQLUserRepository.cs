using ITI.ItSchool.Models.UserEntities;
using ITI.ItSchool.Models;
using ITI.ItSchool.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models
{
    public class SQLUserRepository : IUserRepository
    {
        /// <summary>
        /// Creates a new User add it to the database.
        /// </summary>
        /// <param name="user">The user to create as an object</param>
        /// <returns>The new user who was created.</returns>
        public bool Create( User user )
        {
            if( user == null ) throw new ArgumentNullException( "The 'User' as an object type is null.", "user" );
            using ( var userContext = new UserContext() )
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// Finds a user by his nickname.
        /// 
        /// TO REFACTOR WITH SYNTAXIC SUGAR "USING"
        /// </summary>
        /// <param name="nickname">The user who we look for.</param>
        /// <returns>A list which contains several informations about him.</returns>
        public IList<User> FindByNickname( string nickname )
        {
            IList<User> users = new List<User>();
            UserContext userContext = new UserContext();

            var user = from u in userContext.Users
                       where u.Nickname == nickname
                       select new               // Anonymous type
                       {
                           Nickname = u.Nickname 
                       };

            users = userContext.Users.ToList();
            userContext.Dispose();

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
                userToUpdate[0].Mail = user.Mail;

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
