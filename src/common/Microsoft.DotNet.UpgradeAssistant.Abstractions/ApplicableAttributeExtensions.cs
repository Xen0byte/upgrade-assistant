// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;

namespace Microsoft.DotNet.UpgradeAssistant
{
    public static class ApplicableAttributeExtensions
    {
        /// <summary>
        /// Uses reflection to see if the type has an attribute matching the flag.
        /// </summary>
        /// <param name="aType"></param>
        /// <param name="flag"></param>
        /// <returns>True if the type has the flag. The default is true when no attribute is present.</returns>
        public static bool IsApplicableToFramework(this Type aType, NetFrameworkProject flag)
        {
            if (aType == null)
            {
                return false;
            }

            var applicableAttr = aType.CustomAttributes.FirstOrDefault(a => a.AttributeType.FullName.Equals(typeof(ApplicableNetFrameworkAttribute).FullName, StringComparison.Ordinal));
            if (applicableAttr is not null)
            {
                var netFramework = applicableAttr.ConstructorArguments.Single().Value as int?;
                if (netFramework.HasValue)
                {
                    var netFrameworkFlag = (NetFrameworkProject)netFramework.Value;
                    if (flag == NetFrameworkProject.All && netFramework.Value > 0)
                    {
                        return true;
                    }

                    if (flag == NetFrameworkProject.None && netFrameworkFlag == NetFrameworkProject.None)
                    {
                        return true;
                    }

                    if (flag != netFrameworkFlag)
                    {
                        return false;
                    }
                }
            }

            return flag != NetFrameworkProject.None;
        }
    }
}
