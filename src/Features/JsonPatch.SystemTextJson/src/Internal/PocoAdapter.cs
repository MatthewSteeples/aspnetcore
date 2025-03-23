// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace Microsoft.AspNetCore.JsonPatch.SystemTextJson.Internal;

/// <summary>
/// This API supports infrastructure and is not intended to be used
/// directly from your code. This API may change or be removed in future releases.
/// </summary>
public class PocoAdapter : IAdapter
{
    public virtual bool TryAdd(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        object value,
        out string errorMessage)
    {
        if (!TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
            return false;
        }

        if (jsonProperty.Set == null)
        {
            errorMessage = Resources.FormatCannotUpdateProperty(segment);
            return false;
        }

        if (!TryConvertValue(value, jsonProperty.PropertyType, jsonSerializerOptions, out var convertedValue))
        {
            errorMessage = Resources.FormatInvalidValueForProperty(value);
            return false;
        }

        jsonProperty.Set(target, convertedValue);

        errorMessage = null;
        return true;
    }

    public virtual bool TryGet(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        out object value,
        out string errorMessage)
    {
        if (!TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
            value = null;
            return false;
        }

        if (jsonProperty.Get == null)
        {
            errorMessage = Resources.FormatCannotReadProperty(segment);
            value = null;
            return false;
        }

        value = jsonProperty.Get(target);
        errorMessage = null;
        return true;
    }

    public virtual bool TryRemove(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        out string errorMessage)
    {
        if (!TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
            return false;
        }

        if (jsonProperty.Set == null)
        {
            errorMessage = Resources.FormatCannotUpdateProperty(segment);
            return false;
        }

        // Setting the value to "null" will use the default value in case of value types, and
        // null in case of reference types
        object value = null;
        if (jsonProperty.PropertyType.IsValueType
            && Nullable.GetUnderlyingType(jsonProperty.PropertyType) == null)
        {
            value = Activator.CreateInstance(jsonProperty.PropertyType);
        }

        jsonProperty.Set(target, value);

        errorMessage = null;
        return true;
    }

    public virtual bool TryReplace(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        object value,
        out string errorMessage)
    {
        if (!TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
            return false;
        }

        if (jsonProperty.Set == null)
        {
            errorMessage = Resources.FormatCannotUpdateProperty(segment);
            return false;
        }

        if (!TryConvertValue(value, jsonProperty.PropertyType, jsonSerializerOptions, out var convertedValue))
        {
            errorMessage = Resources.FormatInvalidValueForProperty(value);
            return false;
        }

        jsonProperty.Set(target, convertedValue);

        errorMessage = null;
        return true;
    }

    public virtual bool TryTest(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        object value,
        out string errorMessage)
    {
        if (!TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
            return false;
        }

        if (jsonProperty.Get == null)
        {
            errorMessage = Resources.FormatCannotReadProperty(segment);
            return false;
        }

        if (!TryConvertValue(value, jsonProperty.PropertyType, jsonSerializerOptions, out var convertedValue))
        {
            errorMessage = Resources.FormatInvalidValueForProperty(value);
            return false;
        }

        var currentValue = jsonProperty.Get(target);
        if (!JsonObject.DeepEquals(JsonSerializer.SerializeToNode(currentValue), JsonSerializer.SerializeToNode(convertedValue)))
        {
            errorMessage = Resources.FormatValueNotEqualToTestValue(currentValue, value, segment);
            return false;
        }

        errorMessage = null;
        return true;
    }

    public virtual bool TryTraverse(
        object target,
        string segment,
        JsonSerializerOptions jsonSerializerOptions,
        out object value,
        out string errorMessage)
    {
        if (target == null)
        {
            value = null;
            errorMessage = null;
            return false;
        }

        if (TryGetJsonProperty(target, jsonSerializerOptions, segment, out var jsonProperty))
        {
            value = jsonProperty.Get(target);
            errorMessage = null;
            return true;
        }

        value = null;
        errorMessage = Resources.FormatTargetLocationAtPathSegmentNotFound(segment);
        return false;
    }

    protected virtual bool TryGetJsonProperty(
        object target,
        JsonSerializerOptions jsonSerializerOptions,
        string segment,
        out JsonPropertyInfo jsonProperty)
    {
        var typeInfo = jsonSerializerOptions.GetTypeInfo(target.GetType());
        if (typeInfo is not null)
        {
            var pocoProperty = typeInfo
                .Properties
                .FirstOrDefault(p => string.Equals(p.Name, segment, StringComparison.OrdinalIgnoreCase));

            if (pocoProperty != null)
            {
                jsonProperty = pocoProperty;
                return true;
            }
        }

        jsonProperty = null;
        return false;
    }

    protected virtual bool TryConvertValue(object value, Type propertyType, out object convertedValue)
    {
        return TryConvertValue(value, propertyType, null, out convertedValue);
    }

    protected virtual bool TryConvertValue(object value, Type propertyType, JsonSerializerOptions jsonSerializerOptions, out object convertedValue)
    {
        var conversionResult = ConversionResultProvider.ConvertTo(value, propertyType, jsonSerializerOptions);
        if (!conversionResult.CanBeConverted)
        {
            convertedValue = null;
            return false;
        }

        convertedValue = conversionResult.ConvertedInstance;
        return true;
    }
}
