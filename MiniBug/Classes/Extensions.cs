// Copyright(c) João Martiniano. All rights reserved.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;

namespace MiniBug
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns a human-readable description of a member of a enum.
        /// </summary>
        /// <param name="e">The enum member.</param>
        /// <returns>A string containing the description.</returns>
        public static string ToDescription(this Enum e)
        {
            // This code was adapted from: https://blogs.msdn.microsoft.com/abhinaba/2005/10/21/c-3-0-using-extension-methods-for-enum-tostring/

            Type type = e.GetType();
            MemberInfo[] memInfo = type.GetMember(e.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return e.ToString();
        }
    }
}
