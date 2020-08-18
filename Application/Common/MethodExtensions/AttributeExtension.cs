using System;
using System.Linq;

namespace Application.Common.MethodExtensions
{
    public static class AttributeExtension
    {
        public static TA GetAttributeFrom<TC, TA>(string propertyName) where TA : Attribute =>
            (TA)(typeof(TC).GetProperty(propertyName)
                ?.GetCustomAttributes(typeof(TA), false).SingleOrDefault());


    }
}
