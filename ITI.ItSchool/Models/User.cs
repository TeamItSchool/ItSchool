using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class User
    {
        public int Id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public int Right { get; set; }

        public string Mail { get; set; }

        public int Grade { get; set; }

        public string Password { get; set; }

        public int Avatar { get; set; }

        public string Remarks { get; set; }
    }
}