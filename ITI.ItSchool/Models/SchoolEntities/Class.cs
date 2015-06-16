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
    [Table("Classes")]
    public class Class
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MinLength( 2 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}