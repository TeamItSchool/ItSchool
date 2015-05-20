using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.UserEntities
{
    [Table( "Groups" )]
    public class Group
    {
        List<User> _userList;

        public Group()
        {
            _userList = new List<User>();
        }

        [Key]
        public int GroupId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}