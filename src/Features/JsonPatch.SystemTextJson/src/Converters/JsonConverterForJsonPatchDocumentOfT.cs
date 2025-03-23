// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Microsoft.AspNetCore.JsonPatch.SystemTextJson.Converters;

internal class JsonConverterForJsonPatchDocumentOfT<T> : JsonConverter<JsonPatchDocument<T>> where T : class
{
    public override JsonPatchDocument<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }
        var converter = new TypedJsonPatchDocumentConverter();
        var document = converter.Read(ref reader, typeToConvert, options);
        return document as JsonPatchDocument<T>;
    }

    public override void Write(Utf8JsonWriter writer, JsonPatchDocument<T> value, JsonSerializerOptions options)
    {

    }
}
