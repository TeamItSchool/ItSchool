using ITI.ItSchool.Models.AvatarEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.UserEntity
{
    public class User
    {
        public User() { }

        public int UserId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public int GradeId { get; set; }

        public Grade Grade { get; set; }

        public int RightId { get; set; }

        public Right Right { get; set; }

        public int AvatarId { get; set; }

        public Avatar Avatar { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }

        public string Remarks { get; set; }
    }
}