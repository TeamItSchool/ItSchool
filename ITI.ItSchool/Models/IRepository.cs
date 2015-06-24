﻿using ITI.ItSchool.Models.Entities;
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
        JsonResult GetChildrenByClassId( int id );

        List<User> GetChildrenListByClassId( int id );

        JsonResult FindUserByNickname(string nickname);

        JsonResult GetChapters();

        JsonResult GetClasses();

        JsonResult GetGroups();

        JsonResult GetClozeExerciseContent();
        JsonResult getUsersByClasses(int id);

        JsonResult SetExercise(Exercise exercise);

        User FindByNickname(string nickname);

        User FindById(int id);

        User FindByMail(string mail);

        User FindByGrade(string grade);

        bool Create(User u);

        bool Create(Class g);

        bool Remove(User u);

        bool Remove(int id);

        IList<User> Update(User u);

        JsonResult getBattleCardChoice();
    }
}
