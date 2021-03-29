// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.DotNet.UpgradeAssistant
{
    using System;

    [Flags]
    public enum NetFrameworkProject
    {
        None = 0,
        Net11 = 1,
        Net20 = 1 << 1,
        Net35 = 1 << 2,
        Net40 = 1 << 3,
        Net403 = 1 << 4,
        Net45 = 1 << 5,
        Net451 = 1 << 6,
        Net452 = 1 << 7,
        Net46 = 1 << 8,
        Net461 = 1 << 9,
        Net462 = 1 << 10,
        Net47 = 1 << 11,
        Net471 = 1 << 12,
        Net472 = 1 << 13,
        Net48 = 1 << 14,
        All = ~(-1 << 15)
    }
}
