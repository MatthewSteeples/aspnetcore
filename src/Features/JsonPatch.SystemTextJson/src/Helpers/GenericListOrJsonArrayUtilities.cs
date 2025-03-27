// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace Microsoft.AspNetCore.JsonPatch.SystemTextJson.Helpers;

internal static class GenericListOrJsonArrayUtilities
{
    internal static object GetElementAt(object list, int index)
    {
        if (list is IList nonGenericList)
        {
            return nonGenericList[index];
        }

        if (list is IList<object> genericList)
        {
            return genericList[index];
        }

        throw new InvalidOperationException($"Unsupported list type: {list.GetType()}");
    }

    internal static void SetValueAt(object list, int index, object value)
    {
        if (list is IList nonGenericList)
        {
            nonGenericList[index] = value;
        }
        else if (list is IList<object> genericList)
        {
            genericList[index] = value;
        }

        throw new InvalidOperationException($"Unsupported list type: {list.GetType()}");
    }

    internal static int GetCount(object list)
    {
        if (list is ICollection nonGenericList)
        {
            return nonGenericList.Count;
        }

        if (list is JsonArray jsonArray)
        {
            return jsonArray.Count;
        }

        if (list is IList<object> genericList)
        {
            return genericList.Count;
        }

        throw new InvalidOperationException($"Unsupported list type: {list.GetType()}");
    }

    internal static void RemoveElementAt(object list, int index)
    {
        if (list is IList nonGenericList)
        {
            nonGenericList.RemoveAt(index);
        }
        else if (list is IList<object> genericList)
        {
            genericList.RemoveAt(index);
        }
        else
        {
            throw new InvalidOperationException($"Unsupported list type: {list.GetType()}");
        }
    }
}
