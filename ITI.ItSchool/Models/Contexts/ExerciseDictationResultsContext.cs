using ITI.ItSchool.Models.PlugExercicesResults;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.Contexts
{
    public class ExerciseDictationResultsContext : DbContext
    {
        public ExerciseDictationResultsContext()
            : base("ItSchool") 
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<ExerciseDictationResultsContext>() );
        }

        public DbSet<ExerciseDictationResults> ExerciseDictationResults { get; set; }
    }
}
