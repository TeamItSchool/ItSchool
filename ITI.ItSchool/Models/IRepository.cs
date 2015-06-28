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
        void AffectExercise( List<int> usersIds, int exerciseId );

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

        User FindByMail(string mail);

        User FindByGrade(string grade);

        string CreateExerciseCloze(ExerciseClozeData ec);

        bool Create(User u);

        bool Create(Class c);

        bool Remove(User u);

        bool Remove(int id);

        IList<User> Update(User u);
    }
}
