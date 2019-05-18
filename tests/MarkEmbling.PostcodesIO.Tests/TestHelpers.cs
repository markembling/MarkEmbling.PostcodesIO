using System;

namespace MarkEmbling.PostcodesIO.Tests
{
    static class TestHelpers
    {
        /// <summary>
        /// Gets an instance of an attribute from a specific property on the given type
        /// </summary>
        /// <typeparam name="TAttrib">Attribute type</typeparam>
        /// <param name="classType">Class containing the property</param>
        /// <param name="prop">Property name</param>
        /// <returns>Attribute instance or null</returns>
        public static TAttrib GetPropertyAttribute<TAttrib>(Type classType, string prop) where TAttrib : Attribute
        {
            var pi = classType.GetProperty(prop);
            var attr = (TAttrib[])pi.GetCustomAttributes(typeof(TAttrib), false);
            return attr.Length == 0 ? null : attr[0];
        }
    }
}
