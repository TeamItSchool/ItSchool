using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Chapter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Theme { get; set; }

        public int Grade { get; set; }

        public string Remarks { get; set; }
    }
}