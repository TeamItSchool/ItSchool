using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.SchoolEntities
{
    [Table("Grades")]
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }

        //public virtual ICollection<Chapter> Chapters { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}