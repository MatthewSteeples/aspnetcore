// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Microsoft.AspNetCore.JsonPatch.Converters;

public class TypedJsonPatchDocumentConverter : JsonPatchDocumentConverter
{
    public override object ReadJson(
        Utf8JsonReader reader,
        Type objectType,
        object existingValue,
        JsonSerializer serializer)
    {
        try
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var genericType = objectType.GenericTypeArguments[0];

            // load jObject
            var jObject = JsonArray.Parse(ref reader);

            // Create target object for Json => list of operations, typed to genericType
            var genericOperation = typeof(Operation<>);
            var concreteOperationType = genericOperation.MakeGenericType(genericType);

            var genericList = typeof(List<>);
            var concreteList = genericList.MakeGenericType(concreteOperationType);

            var targetOperations = Activator.CreateInstance(concreteList);

            //Create a new reader for this jObject, and set all properties to match the original reader.
            var jObjectReader = jObject.CreateReader();
            jObjectReader.Culture = reader.Culture;
            jObjectReader.DateParseHandling = reader.DateParseHandling;
            jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
            jObjectReader.FloatParseHandling = reader.FloatParseHandling;

            // Populate the object properties
            serializer.Populate(jObjectReader, targetOperations);

            // container target: the typed JsonPatchDocument.
            var container = Activator.CreateInstance(objectType, targetOperations, JsonPatchDocumentConverter.DefaultContractResolver);

            return container;
        }
        catch (Exception ex)
        {
            throw new JsonSerializationException(Resources.InvalidJsonPatchDocument, ex);
        }
    }
}
