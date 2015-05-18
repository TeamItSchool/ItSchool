using ITI.ItSchool.Models.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [ForeignKey( "FirstUserId" )]
        public int FirstUserId { get; set; }

        public User FirstUser { get; set; }

        [ForeignKey( "SecondUserId" )]
        public int SecondUserId { get; set; }

        public User SecondUser { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}