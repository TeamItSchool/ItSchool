using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class DataAccessLayer : IDataAccessLayer
    {
        DatabaseContext db;

        public DataAccessLayer()
        {
            db = new DatabaseContext();
        }

        //public List<T> GetEntity()
        //{
        //    //switch typeof( T )
        //    //{
        //    //    case Group:
        //    //        return db.Groups.ToList();
        //    //        break;

        //    //    default:
        //    //        return null;
        //    //        break;
        //    //}

        //    throw new NotImplementedException();
        //}

        //public List<T> GetEntityById( int id )
        //{
        //    throw new NotImplementedException();
        //}

        //public List<T> GetEntityByIdentifier( string identifier )
        //{
        //    throw new NotImplementedException();
        //}

        public List<Group> GetGroups()
        {
            return db.Groups.ToList();
        }

        public void CreateGroup( int id, string name, string remarks )
        {
            db.Groups.Add( new Group { Id = id, Name = name, Remarks = remarks } );
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}