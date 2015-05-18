using ITI.ItSchool.Models;
using ITI.ItSchool.Models.UserEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Tests
{
    [TestFixture]
    public class UserContextTests
    {
        [Test]
        public void create_a_user_and_find_him()
        {
            IUserRepository userRepo = new SQLUserRepository();

            User nullUser = null;

            Assert.Throws<ArgumentNullException>( () => userRepo.CreateUser( nullUser ) );

            User u = new User();

            u.FirstName = "Michael";
            u.LastName = "Kyle";
            u.Mail = "kyle@microsoft.com";
            u.Nickname = "Miky";
            u.Password = "mypass";
            u.Remarks = "This is a test...";

            bool isCreated = userRepo.CreateUser( u );

            Assert.That( isCreated, Is.EqualTo( true ) );
        }
    }
}
