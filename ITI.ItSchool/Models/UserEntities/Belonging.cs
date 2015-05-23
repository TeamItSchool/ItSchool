using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.UserEntities
{
    public class Belonging
    {
        [Key]
        public int BelongingId { get; set; }

        public int UserId { get; set; }

        [ForeignKey( "UserId" )]
        public User User { get; set; }

        public int GroupId { get; set; }

        [ForeignKey( "GroupId" )]
        public Group Group { get; set; }

        public DateTime IncomingDate { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}