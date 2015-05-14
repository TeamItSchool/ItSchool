using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.AvatarEntities
{
    public class Avatar
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Mouth { get; set; }

        public int Hair { get; set; }

        public int Nose { get; set; }

        public int Eye { get; set; }

        public int Clothe { get; set; }

        public string Remarks { get; set; }
    }
}