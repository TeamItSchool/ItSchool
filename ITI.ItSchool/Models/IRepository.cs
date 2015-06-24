using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public interface IRepository
    {
        User FindByNickname( string nickname );

        JsonResult GetChildrenByClassId( int id );

        List<User> GetChildrenListByClassId( int id );

        IList<User> FindAllUsers();

        JsonResult FindUserByNickname( string nickname );

        JsonResult GetClasses();

        JsonResult GetGroups();

        JsonResult getUsersByClasses(int id);

        IList<User> Update( User u );
        
        User FindById( int id );
        
        User FindByMail( string mail );

        User FindByGrade( string grade );

        bool Create( User u );

        bool Create( Class g );

        bool Remove( User u );

        bool Remove( int id );

        JsonResult SetExercise( Exercise exercise );

        JsonResult getBattleCardChoice();
    }
}
