using ITI.ItSchool.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Tests
{
    [TestFixture]
    public class DataAccessLayerTests
    {
        [Test]
        public void can_create_a_group_and_get_it()
        {
            using( IDataAccessLayer<Group> dal = new DataAccessLayer<Group>() )
            {
                dal.CreateGroup( 1, "Pupils", "Teacher's group" );
                List<Group> groups = dal.GetGroups();

                Assert.IsNotNull( groups );
                Assert.AreEqual( 3, groups.Count );
                Assert.AreEqual( "Teachers", groups[0].Name );
            }
        }
    }
}
