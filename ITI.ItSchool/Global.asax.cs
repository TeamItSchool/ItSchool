using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using ITI.ItSchool.Models.Contexts;

namespace ITI.ItSchool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<UserContext>( new DropCreateDatabaseIfModelChanges<UserContext>() );
            Database.SetInitializer<ExerciseContext>( new DropCreateDatabaseIfModelChanges<ExerciseContext>() );
        }
    }
}
