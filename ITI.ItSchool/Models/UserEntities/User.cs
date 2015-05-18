using ITI.ItSchool.Models.AvatarEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.UserEntity
{
    [Table( "Users", Schema="ItSchool" )]
    public class User
    {
        public User() { }

        [Key]
        public int UserId { get; set; }

        [MinLength( 3 )]
        [MaxLength( 45 )]
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }

        public int GradeId { get; set; }

        [ForeignKey( "GradeId" )]
        public virtual Grade Grade { get; set; }

        public int RightId { get; set; }

        [ForeignKey( "RightId" )]
        public virtual Right Right { get; set; }

        public int AvatarId { get; set; }

        [ForeignKey( "AvatarId" )]
        public virtual Avatar Avatar { get; set; }

        public int GroupId { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}