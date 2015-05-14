using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int QuantityMembers { get; set; }

        public string Remarks { get; set; }
    }
}