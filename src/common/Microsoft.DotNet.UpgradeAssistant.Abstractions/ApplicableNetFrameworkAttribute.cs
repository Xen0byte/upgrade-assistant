// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.DotNet.UpgradeAssistant
{
    using System;

    /// <summary>
    /// An attribute for marking upgrade assistant features that are only applicable when
    /// the project being upgraded targets a specific framework.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ApplicableNetFrameworkAttribute : Attribute
    {
        /// <summary>
        /// Gets the framework that a project must contain for the attributed feature to apply.
        /// In order for the attributed feature to apply, the project must contain all of
        /// the indicated components (but it can also contain more).
        /// </summary>
        public NetFrameworkProject Framework { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicableFrameworkAttribute"/> class.
        /// </summary>
        /// <param name="framework">The framework that a project must contain for the attributed
        /// feature to apply to it.</param>
        public ApplicableNetFrameworkAttribute(NetFrameworkProject framework)
        {
            Framework = framework;
        }
    }
}
