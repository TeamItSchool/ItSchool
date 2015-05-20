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
        public void create_a_user()
        {
            IUserRepository userRepo = new SQLUserRepository();

            User nullUser = null;

            Assert.Throws<ArgumentNullException>( () => userRepo.CreateUser( nullUser ) );

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

            var user = new User
            {
                UserId = 1,
                FirstName = "Michael",
                LastName = "Junior",
                Nickname = "Tikari",
                Mail = "junior@microsoft.com",
                Password = "mypass",
                Avatar = a,
                Grade = grade,
                Right = right,
                Remarks = "This is a test..."
            };

            bool isCreated = userRepo.CreateUser( user );

            Assert.That( isCreated, Is.EqualTo( true ) );
        }

        [Test]
        public void can_find_a_user()
        {
            string nickname = "Tikari";
            IList<User> u;

            IUserRepository userRepo = new SQLUserRepository();

            u = userRepo.FindByNickname( nickname );
            Assert.That( nickname, Is.EqualTo( u[0].Nickname ) );
        }
    }
}
