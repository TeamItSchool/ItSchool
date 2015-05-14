using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int Conversation { get; set; }

        public DateTime MsgDate { get; set; }

        public string Content { get; set; }

        public string Remarks { get; set; }
    }
}