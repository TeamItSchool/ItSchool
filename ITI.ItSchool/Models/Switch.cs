using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Switch
    {
        public object Target { get; set; }

        public Switch( object target )
        {
            this.Target = target;
        }
    }
}