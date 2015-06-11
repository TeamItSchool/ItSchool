using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.ChatEntities
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public int FirstUserId { get; set; }

        [ForeignKey("FirstUserId") ]
        public virtual User FirstUser { get; set; }

        public int SecondUserId { get; set; }

        [ForeignKey("SecondUserId")]
        public virtual User SecondUser { get; set; }

        //protected override void OnModelCreating( DbModelBuilder modelBuilder )
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        //    modelBuilder.Entity<Conversation>()
        //        .HasRequired(f => f.SecondUser)
        //        .WithRequiredDependent()
        //        .WillCascadeOnDelete(false);
        //}
    }
}