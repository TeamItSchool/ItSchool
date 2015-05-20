using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models
{
    public interface IUserRepository
    {
        User FindById( int id );

        IList<User> FindByNickname( string nickname );

        User FindByMail( string mail );

        User FindByGrade( string grade );

        bool CreateUser( User u );

        bool Remove( User u );

        bool Remove( int id );
    }
}
