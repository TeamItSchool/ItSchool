using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class DataAccessLayer<T> : IDataAccessLayer<T>
    {
        DatabaseContext db;
        Switch sw;

        public DataAccessLayer()
        {
            db = new DatabaseContext();
        }

        public List<T> GetEntity()
        {
            sw = new Switch(  )
        }

        public List<T> GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetEntityByIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

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