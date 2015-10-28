using ITI.ItSchool.Models;
using ITI.ItSchool.Models.AvatarEntities;
using ITI.ItSchool.Models.Contexts;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            Console.WriteLine("Swag");
        }

        private void initialize_database_exerciseContext()
        {
            IDatabaseInitializer<ExerciseContext> init = new DropCreateDatabaseAlways<ExerciseContext>();
            Database.SetInitializer( init );
            init.InitializeDatabase( new ExerciseContext() );
        }

        //[Test]
        //public void create_a_null_user_throws_ArgumentNullException()
        //{
        //    IRepository repo = new SQLRepository();

        //    User nullUser = null;

        //    Assert.Throws<ArgumentNullException>( () => repo.Create( nullUser ) );
        //}

        //[Test]
        //public void cannot_create_a_user_with_an_existing_nickname()
        //{
        //    initialize_database_userContext();

        //    IRepository repo = new SQLRepository();

        //    var body = new Body
        //    {
        //        Name = "Soldat"
        //    };

        //    var foot = new Foot
        //    {
        //        Name = "Soldat"
        //    };

        //    var legs = new Legs
        //    {
        //        Name = "Soldat"
        //    };

        //    var grade = new Class
        //    {
        //        Name = "TestGrade",
        //        Description = "TestRemarkGrade"
        //    };

        //    var a = new Avatar
        //    {
        //        Name = "TestAvatar",
        //        Body = body,
        //        Feet = foot,
        //        Legs = legs
        //    };

        //    var g = new Group
        //    {
        //        Name = "TestGroup",
        //        Remarks = "GroupRemarks..."
        //    };

        //    var user = new User
        //    {
        //        UserId = 1,
        //        FirstName = "Guénolé",
        //        LastName = "Kikabou",
        //        Nickname = "guenole_k",
        //        Mail = "kikabouguenole@gmail.com",
        //        Password = "admin",
        //        Avatar = a,
        //        Group = g,
        //        Class = grade,
        //        Remarks = "This is a test..."
        //    };

        //    bool isCreated = repo.Create( user );

        //    Assert.That( isCreated == true );

        //    var sameUser = new User
        //    {
        //        UserId = 1,
        //        FirstName = "Guénolé",
        //        LastName = "Kikabou",
        //        Nickname = "guenole_k",
        //        Mail = "kikabouguenole@gmail.com",
        //        Password = "admin",
        //        Avatar = a,
        //        Group = g,
        //        Class = grade,
        //        Remarks = "This is a test..."
        //    };

        //    isCreated = repo.Create( sameUser );

        //    Assert.That( isCreated == false );

        //}

        //[Test]
        //public void create_a_user()
        //{
        //    initialize_database_userContext();
        //    IRepository repo = new SQLRepository();

        //    var body = new Body
        //    {
        //        Name = "Soldat"
        //    };

        //    var foot = new Foot
        //    {
        //        Name = "Soldat"
        //    };

        //    var legs = new Legs
        //    {
        //        Name = "Soldat"
        //    };

        //    var grade = new Class
        //    {
        //        Name = "TestGrade",
        //        Description = "TestRemarkGrade"
        //    };

        //    var a = new Avatar
        //    {
        //        Name = "TestAvatar",
        //        Body = body,
        //        Feet = foot,
        //        Legs = legs
        //    };

        //    var g = new Group
        //    {
        //        Name = "TestGroup",
        //        Remarks = "GroupRemarks..."
        //    };

        //    var user = new User
        //    {
        //        UserId = 1,
        //        FirstName = "Guénolé",
        //        LastName = "Kikabou",
        //        Nickname = "guenole_k",
        //        Mail = "kikabouguenole@gmail.com",
        //        Password = "admin",
        //        Avatar = a,
        //        Class = grade,
        //        Group = g,
        //        Remarks = "This is a test..."
        //    };

        //    bool isCreated = repo.Create( user );

        //    Assert.That( isCreated, Is.EqualTo( true ) );
        //}

        //[Test]
        //public void can_find_a_user()
        //{
        //    initialize_database_userContext();
        //    IRepository urepo = new SQLRepository();

        //    var body = new Body
        //    {
        //        Name = "Soldat"
        //    };

        //    var foot = new Foot
        //    {
        //        Name = "Soldat"
        //    };

        //    var legs = new Legs
        //    {
        //        Name = "Soldat"
        //    };

        //    var grade = new Class
        //    {
        //        Name = "TestGrade",
        //        Description = "TestRemarkGrade"
        //    };

        //    var a = new Avatar
        //    {
        //        Name = "TestAvatar",
        //        Body = body,
        //        Feet = foot,
        //        Legs = legs
        //    };

        //    var g = new Group
        //    {
        //        Name = "TestGroup",
        //        Remarks = "GroupRemarks..."
        //    };

        //    var user = new User
        //    {
        //        UserId = 1,
        //        FirstName = "Guénolé",
        //        LastName = "Kikabou",
        //        Nickname = "Toto",
        //        Mail = "ms@gmail.com",
        //        Password = "admin",
        //        Avatar = a,
        //        Group = g,
        //        Class = grade,
        //        Remarks = "This is a test..."
        //    };
        //    string nickname = user.Nickname;
        //    User u;
        //    bool isCreated = urepo.Create( user );

        //    Assert.That( isCreated == true );

        //    u = urepo.FindByNickname( nickname );

        //    Assert.That( nickname, Is.EqualTo( u.Nickname ) );
        //}

        //[Test]
        //public void can_update_a_user()
        //{
        //    initialize_database_userContext();
        //    IRepository repo = new SQLRepository();
        //    IList<User> u;

        //    var body = new Body
        //    {
        //        Name = "Soldat"
        //    };

        //    var foot = new Foot
        //    {
        //        Name = "Soldat"
        //    };

        //    var legs = new Legs
        //    {
        //        Name = "Soldat"
        //    };

        //    var grade = new Class
        //    {
        //        Name = "TestGrade",
        //        Description = "TestRemarkGrade"
        //    };

        //    var a = new Avatar
        //    {
        //        Name = "TestAvatar",
        //        Body = body,
        //        Feet = foot,
        //        Legs = legs
        //    };

        //    var g = new Group
        //    {
        //        Name = "TestGroup",
        //        Remarks = "GroupRemarks..."
        //    };

        //    var user = new User
        //    {
        //        FirstName = "John",
        //        LastName = "Smith",
        //        Nickname = "Loulou",
        //        Mail = "smith@microsoft.com",
        //        Password = "mypass",
        //        Avatar = a,
        //        Class = grade,
        //        Group = g,
        //        Remarks = "This is a test..."
        //    };

        //    bool isCreated = repo.Create( user );

        //    Assert.That( isCreated == true );

        //    u = repo.FindAllUsers();

        //    Assert.That( user.Mail, Is.EqualTo( "smith@microsoft.com" ) );

        //    user.Mail = "john.smith@outlook.com";

        //    u = repo.Update( user );

        //    Assert.That( user.Mail, Is.EqualTo( "john.smith@outlook.com" ) );

        //    user.Password = "mypassword";

        //    u = repo.Update( user );

        //    Assert.That( user.Mail, Is.EqualTo( "john.smith@outlook.com" ) );
        //    Assert.IsNull( u );
        //}

        //[Test]
        //public void cannot_create_an_account_with_an_existing_mail()
        //{
        //    initialize_database_userContext();

        //    IRepository repo = new SQLRepository();

        //    var body = new Body
        //    {
        //        Name = "Soldat"
        //    };

        //    var foot = new Foot
        //    {
        //        Name = "Soldat"
        //    };

        //    var legs = new Legs
        //    {
        //        Name = "Soldat"
        //    };

        //    var grade = new Class
        //    {
        //        Name = "TestGrade",
        //        Description = "TestRemarkGrade"
        //    };

        //    var a = new Avatar
        //    {
        //        Name = "TestAvatar",
        //        Body = body,
        //        Feet = foot,
        //        Legs = legs
        //    };

        //    var g = new Group
        //    {
        //        Name = "TestGroup",
        //        Remarks = "GroupRemarks..."
        //    };

        //    var user = new User
        //    {
        //        FirstName = "Antoine",
        //        LastName = "Raqs",
        //        Nickname = "Toinou",
        //        Mail = "raqs@wanadoo.fr",
        //        Password = "mypass",
        //        Avatar = a,
        //        Class = grade,
        //        Group = g,
        //        Remarks = "This is a test..."
        //    };

        //    bool isCreated = repo.Create(user);

        //    Assert.That( isCreated == true );

        //    var userWithSameMailAdd = new User
        //    {
        //        FirstName = "Jean",
        //        LastName = "Romain",
        //        Nickname = "Reynolds",
        //        Mail = "raqs@wanadoo.fr",
        //        Password = "mypass",
        //        Avatar = a,
        //        Class = grade,
        //        Group = g,
        //        Remarks = "This is a test..."
        //    };

        //    isCreated = repo.Create( userWithSameMailAdd );

        //    Assert.That( isCreated, Is.EqualTo( false ) );

        //    var userWithEmptyFields = new User
        //    {
        //        FirstName = "",
        //        LastName = "",
        //        Nickname = "", 
        //        Mail = "",
        //        Password = "",
        //        Avatar = a,
        //        Group = g, 
        //        Class = grade,
        //        Remarks = "Test of creating user with specific empty fields"
        //    };

        //    isCreated = repo.Create( userWithEmptyFields );

        //    Assert.That( isCreated, Is.Not.EqualTo( true ) );
        //}

        //[Test]
        //public void create_an_exercise()
        //{
        //    initialize_database_exerciseContext();

        //    IRepository repo = new SQLRepository();

        //    var grade = new Class
        //    {
        //        _ClassId = 1,
        //        Name = "6e2",
        //        Description = "Collège"
        //    };

        //    var matter = new Matter
        //    {
        //        MatterId = 1,
        //        Name = "Anglais"
        //    };

        //    var theme = new Theme
        //    {
        //        ThemeId = 1,
        //        Name = "Anglais",
        //        Matter = matter,
        //        MatterId = matter.MatterId
        //    };

        //    var chapter = new Chapter
        //    {
        //        Name = "Verbes en anglais",
        //        Theme = theme,
        //        ThemeId = theme.ThemeId,
        //        Class = grade,
        //        ClassId = grade._ClassId
        //    };

        //    var level = new Level
        //    {
        //        Name = "Facile"
        //    };

        //    var gameType = new ExerciseType
        //    {
        //        Name = "Texte à trous"
        //    };

        //    var exercise = new Exercise
        //    {
        //        Name = "Retrouver les verbes",
        //        Chapter = chapter,
        //        Level = level,
        //        ExerciseType = gameType,
        //        Data = " Hello! My name is Brian. "
        //    };

        //    bool isCreated = repo.Create( exercise );
        //    Assert.That( isCreated, Is.EqualTo( true ) );
        //}

        //[Test]
        //public void guegue_says_to_me_that_we_cant_create_an_alone_grade() 
        //{
        //    IRepository repo = new SQLRepository();

        //    List<Chapter> c = new List<Chapter>();

        //    ICollection<Chapter> chapters = new List<Chapter>();

        //    var matter = new Matter
        //    {
        //        MatterId = 1,
        //        Name = "Maths",
        //    };

        //    var theme = new Theme
        //    {
        //        ThemeId = 1,
        //        Name = "Algèbre",
        //        MatterId = 1,
        //        Remarks = "Jean Neymar"
        //    };

            
        //    var chapter = new Chapter
        //    {
        //        Name = "Les nombres relatifs",
        //        Theme = theme,
        //        ThemeId = 1,
        //        ClassId = 1,
        //    };

        //    c = chapters.ToList();
        //    c.Add(chapter);

        //    var grade = new Class
        //    {
        //        Name = "CM1",
        //        Description = "Go fuck YS"
        //    };

        //    c[0].ClassId = grade._ClassId;

        //    bool isCreated = repo.Create( grade );
        //    Assert.That( isCreated == true );
        //}
    }
}