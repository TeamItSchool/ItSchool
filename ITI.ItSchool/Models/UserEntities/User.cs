using ITI.ItSchool.Models;
using ITI.ItSchool.Models.PlugExercises;
using ITI.ItSchool.Models.SchoolEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Objects;

namespace ITI.ItSchool.Models.UserEntities
{
    [Table( "Users" )]
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

        public int ClassId { get; set; }

        [ForeignKey( "ClassId" )]
        public virtual Class Class { get; set; }

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