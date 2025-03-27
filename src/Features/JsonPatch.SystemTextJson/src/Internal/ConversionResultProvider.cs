// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Microsoft.AspNetCore.JsonPatch.SystemTextJson.Internal;

/// <summary>
/// This API supports infrastructure and is not intended to be used
/// directly from your code. This API may change or be removed in future releases.
/// </summary>
public static class ConversionResultProvider
{
    public static ConversionResult ConvertTo(object value, Type typeToConvertTo)
    {
        return ConvertTo(value, typeToConvertTo, null);
    }

    internal static ConversionResult ConvertTo(object value, Type typeToConvertTo, JsonSerializerOptions jsonSerializerOptions)
    {
        if (value == null)
        {
            return new ConversionResult(IsNullableType(typeToConvertTo), null);
        }

        if (typeToConvertTo.IsAssignableFrom(value.GetType()))
        {
            // No need to convert
            return new ConversionResult(true, value);
        }

        // Workaround for the https://github.com/dotnet/runtime/issues/113926
        if (typeToConvertTo.Name == "JsonValuePrimitive`1")
        {
            typeToConvertTo = typeof(JsonNode);
        }

        try
        {
            var serializedDocument = JsonSerializer.Serialize(value, jsonSerializerOptions);
            var deserialized = JsonSerializer.Deserialize(serializedDocument, typeToConvertTo, jsonSerializerOptions);
            return new ConversionResult(true, deserialized);
        }
        catch (Exception ex)
        {
            return new ConversionResult(canBeConverted: false, convertedInstance: null);
        }
    }

    public static ConversionResult CopyTo(object value, Type typeToConvertTo)
    {
        var targetType = typeToConvertTo;
        if (value == null)
        {
            return new ConversionResult(canBeConverted: true, convertedInstance: null);
        }

        if (typeToConvertTo != value.GetType() && typeToConvertTo.IsAssignableFrom(value.GetType()))
        {
            // Keep original type
            targetType = value.GetType();
        }

        // Workaround for the https://github.com/dotnet/runtime/issues/113926
        if (targetType.Name == "JsonValuePrimitive`1")
        {
            targetType = typeof(JsonNode);
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize(JsonSerializer.Serialize(value), targetType);
            return new ConversionResult(true, deserialized);
        }
        catch (Exception ex)
        {
            return new ConversionResult(canBeConverted: false, convertedInstance: null);
        }
    }

    private static bool IsNullableType(Type type)
    {
        if (type.IsValueType)
        {
            // value types are only nullable if they are Nullable<T>
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        // reference types are always nullable
        return true;
    }
}
