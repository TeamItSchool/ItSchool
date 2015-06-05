using ITI.ItSchool.Models;
using ITI.ItSchool.Models.AvatarEntities;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.UserEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Tests
{
    [TestFixture]
    public class ContextTests
    {
        private void initialize_database_userContext()
        {
            IDatabaseInitializer<UserContext> init = new DropCreateDatabaseAlways<UserContext>();
            Database.SetInitializer( init );
            init.InitializeDatabase( new UserContext() );
        }

        private void initialize_database_gameContext()
        {
            IDatabaseInitializer<GameContext> init = new DropCreateDatabaseAlways<GameContext>();
            Database.SetInitializer( init );
            init.InitializeDatabase( new GameContext() );
        }

        [Test]
        public void create_a_null_user_throws_ArgumentNullException()
        {
            IRepository repo = new SQLRepository();

            User nullUser = null;

            Assert.Throws<ArgumentNullException>( () => repo.Create( nullUser ) );
        }

        [Test]
        public void cannot_create_a_user_with_an_existing_nickname()
        {
            initialize_database_userContext();

            IRepository repo = new SQLRepository();

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
                Remarks = "This is a test..."
            };

            bool isCreated = repo.Create( user );

            Assert.That( isCreated == true );

            var sameUser = new User
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
                Remarks = "This is a test..."
            };

            isCreated = repo.Create( sameUser );

            Assert.That( isCreated == false );

        }

        [Test]
        public void create_a_user()
        {
            initialize_database_userContext();
            IRepository repo = new SQLRepository();

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
                Group = g,
                Remarks = "This is a test..."
            };

            bool isCreated = repo.Create( user );

            Assert.That( isCreated, Is.EqualTo( true ) );
        }

        [Test]
        public void can_find_a_user()
        {
            initialize_database_userContext();
            IRepository urepo = new SQLRepository();

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
                Group = g,
                Grade = grade,
                Remarks = "This is a test..."
            };
            string nickname = user.Nickname;
            User u;
            bool isCreated = urepo.Create( user );

            Assert.That( isCreated == true );

            u = urepo.FindByNickname( nickname );

            Assert.That( nickname, Is.EqualTo( u.Nickname ) );
        }

        [Test]
        public void can_update_a_user()
        {
            initialize_database_userContext();
            IRepository repo = new SQLRepository();
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
                Group = g,
                Remarks = "This is a test..."
            };

            bool isCreated = repo.Create( user );

            Assert.That( isCreated == true );

            u = repo.FindAllUsers();

            Assert.That( user.Mail, Is.EqualTo( "smith@microsoft.com" ) );

            user.Mail = "john.smith@outlook.com";

            u = repo.Update( user );

            Assert.That( user.Mail, Is.EqualTo( "john.smith@outlook.com" ) );

            user.Password = "mypassword";

            u = repo.Update( user );

            Assert.That( user.Mail, Is.EqualTo( "john.smith@outlook.com" ) );
            Assert.IsNull( u );
        }

        [Test]
        public void cannot_create_an_account_with_an_existing_mail()
        {
            initialize_database_userContext();

            IRepository repo = new SQLRepository();

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
                FirstName = "Antoine",
                LastName = "Raqs",
                Nickname = "Toinou",
                Mail = "raqs@wanadoo.fr",
                Password = "mypass",
                Avatar = a,
                Grade = grade,
                Group = g,
                Remarks = "This is a test..."
            };

            bool isCreated = repo.Create(user);

            Assert.That( isCreated == true );

            var userWithSameMailAdd = new User
            {
                FirstName = "Jean",
                LastName = "Romain",
                Nickname = "Reynolds",
                Mail = "raqs@wanadoo.fr",
                Password = "mypass",
                Avatar = a,
                Grade = grade,
                Group = g,
                Remarks = "This is a test..."
            };

            isCreated = repo.Create( userWithSameMailAdd );

            Assert.That( isCreated, Is.EqualTo( false ) );

            var userWithEmptyFields = new User
            {
                FirstName = "",
                LastName = "",
                Nickname = "", 
                Mail = "",
                Password = "",
                Avatar = a,
                Group = g, 
                Grade = grade,
                Remarks = "Test of creating user with specific empty fields"
            };

            isCreated = repo.Create( userWithEmptyFields );

            Assert.That( isCreated, Is.Not.EqualTo( true ) );
        }

        [Test]
        public void create_an_exercise()
        {
            //initialize_database_gameContext();

            IRepository repo = new SQLRepository();

            var chapter = new Chapter
            {
                Name = "Verbes en anglais",
                ThemeId = 1,
                GradeId = 2
            };

            var grade = new Grade
            {
                Name = "6e",
                Remarks = "Collège"
            };

            var theme = new Theme
            {
                Name = "Anglais",
                MatterId = 1
            };

            var matter = new Matter
            {
                Name = "Anglais"
            };

            var level = new Level
            {
                Name = "Facile"
            };

            var gameType = new ExerciseType
            {
                Name = "Texte à trous"
            };

            var game = new Game
            {
                Name = "Retrouver les verbes",
                Chapter = chapter,
                Level = level,
                ExerciseType = gameType,
                Data = " Hello! My name is Brian. "
            };

            bool isCreated = repo.Create( game );
            Assert.That( isCreated, Is.EqualTo( true ) );
        }
    }
}