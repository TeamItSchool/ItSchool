using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public static class SwitchExtensions
    {
        public static Switch Case<T>( this Switch type, Action<T> action ) where T: class
        {
            if( type == null ) return null;

            var t = type.Target as T;
            if ( t == null ) return null;
            
            action( t );
            return null;
        }

        public static void Default( this Switch type, Action action )
        {
            if( type == null ) return;
            action();
        }
    }
}