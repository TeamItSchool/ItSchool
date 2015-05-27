using ITI.ItSchool.Models;
using ITI.ItSchool.Models.AvatarEntities;
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
        public void create_a_null_user_throws_ArgumentNullException()
        {
            IUserRepository userRepo = new SQLUserRepository();

            User nullUser = null;

            Assert.Throws<ArgumentNullException>( () => userRepo.Create( nullUser ) );
        }

        [Test]
        public void cannot_create_a_user_with_an_existing_nickname()
        {
            IUserRepository userRepo = new SQLUserRepository();

            var body = new Body
            {
                Name = "Soldat"
            };

            var foot = new Foot
            {
                Name = "Soldat"
            };

            var legs = new Legs
            {
                Name = "Soldat"
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
                Body = body,
                Feet = foot,
                Legs = legs
            };

            var g = new Group
            {
                Name = "TestGroup",
                Remarks = "GroupRemarks..."
            };

            var user = new User
            {
                UserId = 1,
                FirstName = "Guénolé",
                LastName = "Kikabou",
                Nickname = "guenole_k",
                Mail = "kikabouguenole@gmail.com",
                Password = "admin",
                Avatar = a,
                Group = g,
                Grade = grade,
                Right = right,
                Remarks = "This is a test..."
            };

            bool isCreated = userRepo.Create(user);

            Assert.That(isCreated == false);
        }

        [Test]
        public void create_a_user()
        {
            IUserRepository userRepo = new SQLUserRepository();

            var body = new Body
            {
                Name = "Soldat"
            };

            var foot = new Foot
            {
                Name = "Soldat"
            };

            var legs = new Legs
            {
                Name = "Soldat"
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
                Body = body,
                Feet = foot,
                Legs = legs
            };

            var g = new Group
            {
                Name = "TestGroup",
                Remarks = "GroupRemarks..."
            };

            var user = new User
            {
                UserId = 1,
                FirstName = "Guénolé",
                LastName = "Kikabou",
                Nickname = "guenole_k",
                Mail = "kikabouguenole@gmail.com",
                Password = "admin",
                Avatar = a,
                Grade = grade,
                Right = right,
                Remarks = "This is a test..."
            };

            bool isCreated = userRepo.Create( user );

            Assert.That( isCreated, Is.EqualTo( true ) );
        }

        [Test]
        public void can_find_a_user()
        {
            IUserRepository userRepo = new SQLUserRepository();

            var body = new Body
            {
                Name = "Soldat"
            };

            var foot = new Foot
            {
                Name = "Soldat"
            };

            var legs = new Legs
            {
                Name = "Soldat"
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
                Body = body,
                Feet = foot,
                Legs = legs
            };

            var g = new Group
            {
                Name = "TestGroup",
                Remarks = "GroupRemarks..."
            };

            var user = new User
            {
                UserId = 1,
                FirstName = "Guénolé",
                LastName = "Kikabou",
                Nickname = "Toto",
                Mail = "ms@gmail.com",
                Password = "admin",
                Avatar = a,
                Grade = grade,
                Right = right,
                Remarks = "This is a test..."
            };
            string nickname = user.Nickname;
            User u;
            bool isCreated = userRepo.Create( user );

            Assert.That( isCreated == true );

            u = userRepo.FindByNickname( nickname );

            Assert.That( nickname, Is.EqualTo( u.Nickname ) );
        }

        [Test]
        public void can_update_a_user()
        {
            IUserRepository userRepo = new SQLUserRepository();
            IList<User> u;

            var body = new Body
            {
                Name = "Soldat"
            };

            var foot = new Foot
            {
                Name = "Soldat"
            };

            var legs = new Legs
            {
                Name = "Soldat"
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
                Body = body,
                Feet = foot,
                Legs = legs
            };

            var g = new Group
            {
                Name = "TestGroup",
                Remarks = "GroupRemarks..."
            };

            var user = new User
            {
                FirstName = "John",
                LastName = "Smith",
                Nickname = "Loulou",
                Mail = "smith@microsoft.com",
                Password = "mypass",
                Avatar = a,
                Grade = grade,
                Right = right,
                Group = g,
                Remarks = "This is a test..."
            };

            bool isCreated = userRepo.Create( user );

            Assert.That( isCreated == true );

            u = userRepo.FindAllUsers();

            Assert.That( user.Mail, Is.EqualTo( "smith@microsoft.com" ) );

            user.Mail = "john.smith@outlook.com";

            u = userRepo.Update( user );

            Assert.That( user.Mail, Is.EqualTo( "john.smith@outlook.com" ) );

            user.Password = "mypassword";

            u = userRepo.Update( user );

            Assert.IsNull( u );
        }
    }
}