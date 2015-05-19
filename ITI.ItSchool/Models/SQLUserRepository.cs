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
        public bool CreateUser( User user )
        {
            if( user == null ) throw new ArgumentNullException( "The 'User' as an object type is null.", "user" );
            try
            {
                using (var userContext = new UserContext())
                {
                    userContext.Users.Add(user);
                    userContext.SaveChanges();
                    return true;
                }
            } 
            catch( System.Data.Entity.Validation.DbEntityValidationException ex )
            {
                throw;
            }
        }

        public User FindByNickname( string nickname )
        {
            using( var userContext = new UserContext() ) 
            {
                var user = from u in userContext.Users
                           where u.Nickname == nickname
                           select u;

                return (User)user;
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
