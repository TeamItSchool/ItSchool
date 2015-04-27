using ITI.ItSchool.Controllers;
using NUnit.Framework;
using System;
using System.Web.Mvc;

namespace ITI.ItSchool.Tests
{
    [TestFixture]
    public class BackOfficeControllerTest
    {
        [Test]
        public void a_little_test_for_dummies_if_we_have_the_right_ViewResult() 
        {
            var controller = new BackOfficeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual( "Index", result.ViewName );
        }
    }
}
