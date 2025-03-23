// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.JsonPatch.SystemTextJson.Internal;
using Microsoft.AspNetCore.Shared;

namespace Microsoft.AspNetCore.JsonPatch.SystemTextJson.Adapters;

/// <summary>
/// The default AdapterFactory to be used for resolving <see cref="IAdapter"/>.
/// </summary>
public class AdapterFactory : IAdapterFactory
{
    internal static AdapterFactory Default { get; } = new();

    /// <inheritdoc />
#pragma warning disable PUB0001
    public virtual IAdapter Create(object target)
#pragma warning restore PUB0001
    {
        ArgumentNullThrowHelper.ThrowIfNull(target);

        return target switch
        {
            JsonObject => new JsonObjectAdapter(),
            IList => new ListAdapter(),
            _ => new PocoAdapter()
        };
    }
}

