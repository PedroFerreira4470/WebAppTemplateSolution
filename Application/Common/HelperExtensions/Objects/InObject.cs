using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Common.HelperExtensions.Objects
{
    //Check if is in one if the objects
    public static class  InObject
    {
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");

            return list.Contains(source);
        }


    }
}
