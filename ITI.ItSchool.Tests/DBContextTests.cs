using ITI.ItSchool.Models;
using ITI.ItSchool.Models.UserEntity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Tests
{
    [TestFixture]
    public class DBContextTests
    {
        [Test]
        public void create_database_with_userContext()
        {
            using( var db = new UserContext() )
            {
                var user = new User() { FirstName = "Michael", LastName = "Kyle", Mail = "kyle@microsoft.com" };
                db.Users.Add( user );
                db.SaveChanges();
            }
        }
    }
}
