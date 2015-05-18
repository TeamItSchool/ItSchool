using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.ItSchool.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [ForeignKey( "ConversationId" )]
        public int ConversationId { get; set; }

        public Conversation Conversation { get; set; }

        public DateTime MsgDate { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 255 )]
        public string Content { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}