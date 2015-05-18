using ITI.ItSchool.Models.UserEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Friendship
    {
        [Key]
        public int FriendshipId { get; set; }

        public int FirstUserId { get; set; }

        [ForeignKey( "FirstUserId" )]
        public User FirstUser { get; set; }

        public int SecondUserId { get; set; }

        [ForeignKey("SecondUserId")]
        public User SecondUser { get; set; }

        public DateTime DateOfFriendship { get; set; }

        public int StatusId { get; set; }

        [ForeignKey( "StatusId" )]
        public Status Status { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}