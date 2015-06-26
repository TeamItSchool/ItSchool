using ITI.ItSchool.Models.ClassExercicesPlug;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public interface IRepository
    {
        List<User> GetChildrenListByClassId( int id );

        List<int> GetChildrenListIdByClassId( int id );

        JsonResult GetChildrenByClassId(int id);

        JsonResult FindUserByNickname(string nickname);

        JsonResult GetChapters();

        JsonResult GetClasses();

        JsonResult GetGroups();

        JsonResult GetClozeExerciseContent();

        JsonResult getUsersByClasses(int id);

        JsonResult SetExercise(Exercise exercise);

        JsonResult getBattleCardChoice();

        User FindByNickname(string nickname);

        User FindById(int id);

        User FindByMail(string mail);

        User FindByGrade(string grade);

        string CreateClozeExercise( ExerciseClozeData ec );

        bool Create(User u);

        bool Create(Class g);

        bool Remove(User u);

        bool Remove(int id);

        IList<User> Update(User u);
    }
}
