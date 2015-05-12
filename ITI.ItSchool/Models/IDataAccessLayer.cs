using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models
{
    public interface IDataAccessLayer<T> : IDisposable
    {
        List<T> GetEntity();

        List<T> GetEntityById(int id);

        List<T> GetEntityByIdentifier(string identifier);

        List<Group> GetGroups();

        void CreateGroup( int id, string name, string remarks );
    }
}
