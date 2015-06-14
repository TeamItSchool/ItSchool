using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ITI.ItSchool.Models.ChatEntities;

namespace ITI.ItSchool.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public int ConversationId { get; set; }

        [ForeignKey("ConversationId")]
        public Conversation Conversation { get; set; }

        public DateTime MsgDate { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 255 )]
        public string Content { get; set; }
    }
}