﻿using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ITI.ItSchool.Models
{
    public interface IUserRepository
    {
        User FindByNickname( string nickname );

        IList<User> FindAllUsers();

        JsonResult FindUserByNickname( string nickname );

        IList<User> Update( User u );
        
        User FindById( int id );
        
        User FindByMail( string mail );

        User FindByGrade( string grade );

        bool Create( User u );

        bool Remove( User u );

        bool Remove( int id );
    }
}
