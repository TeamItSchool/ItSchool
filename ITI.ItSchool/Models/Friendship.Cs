using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Friendship
    {
        public int Id { get; set; }

        public int User1 { get; set; }

        public int User2 { get; set; }

        public DateTime DateOfFriendship { get; set; }

        public int Status { get; set; }

        public string Remarks { get; set; }
    }
}