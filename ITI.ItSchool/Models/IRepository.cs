using ITI.ItSchool.Models.ClassExercicesPlug;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public interface IRepository
    {

        List<User> GetChildrenListByClassId(int id);

        List<int> GetChildrenListIdByClassId(int id);

        JsonResult GetChildrenByClassId(int id);

        JsonResult FindUserByNickname(string nickname);

        User FindById(int id);

        JsonResult SaveDictation(ExerciseDictationData dictationData);

        JsonResult SaveBattleCard(ExerciseBattleCardData battleCardData);

        JsonResult GetChapters();

        List<ExerciseAffectation> GetExerciseAffectationListByUserId(int id);

        List<ExerciseDictation> GetExerciseDictationListById(List<int> IDs);

        ExerciseDictation FindExerciseDictationById( int id );
        List<ExerciseBattleCard> GetExerciseBattleCardListById(List<int> IDs);

        JsonResult GetClasses();

        JsonResult GetGroups();

        JsonResult GetClozeExerciseContent(string exerciseName);

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

        ExerciseDictation FindExerciseDictationByLevelId( int levelID );
    }
}
