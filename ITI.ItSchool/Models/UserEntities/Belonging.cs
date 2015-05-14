using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Belonging
    {
        public int Id { get; set; }

        public int User { get; set; }

        public int Group { get; set; }

        public DateTime IncomingDate { get; set; }

        public string Remarks { get; set; }
    }
}