﻿//HintName: OpenApiXmlCommentSupport.generated.cs
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#nullable enable
// Suppress warnings about obsolete types and members
// in generated code
#pragma warning disable CS0612, CS0618

namespace System.Runtime.CompilerServices
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    file sealed class InterceptsLocationAttribute : System.Attribute
    {
        public InterceptsLocationAttribute(int version, string data)
        {
        }
    }
}

namespace Microsoft.AspNetCore.OpenApi.Generated
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.OpenApi;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Microsoft.OpenApi.Models.References;
    using Microsoft.OpenApi.Any;

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file record XmlComment(
        string? Summary,
        string? Description,
        string? Remarks,
        string? Returns,
        string? Value,
        bool Deprecated,
        List<string>? Examples,
        List<XmlParameterComment>? Parameters,
        List<XmlResponseComment>? Responses);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file record XmlParameterComment(string? Name, string? Description, string? Example, bool Deprecated);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file record XmlResponseComment(string Code, string? Description, string? Example);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file sealed record MemberKey(
        Type? DeclaringType,
        MemberType MemberKind,
        string? Name,
        Type? ReturnType,
        Type[]? Parameters) : IEquatable<MemberKey>
    {
        public bool Equals(MemberKey? other)
        {
            if (other is null) return false;

            // Check member kind
            if (MemberKind != other.MemberKind) return false;

            // Check declaring type, handling generic types
            if (!TypesEqual(DeclaringType, other.DeclaringType)) return false;

            // Check name
            if (Name != other.Name) return false;

            // For methods, check return type and parameters
            if (MemberKind == MemberType.Method)
            {
                if (!TypesEqual(ReturnType, other.ReturnType)) return false;
                if (Parameters is null || other.Parameters is null) return false;
                if (Parameters.Length != other.Parameters.Length) return false;

                for (int i = 0; i < Parameters.Length; i++)
                {
                    if (!TypesEqual(Parameters[i], other.Parameters[i])) return false;
                }
            }

            return true;
        }

        private static bool TypesEqual(Type? type1, Type? type2)
        {
            if (type1 == type2) return true;
            if (type1 == null || type2 == null) return false;

            if (type1.IsGenericType && type2.IsGenericType)
            {
                return type1.GetGenericTypeDefinition() == type2.GetGenericTypeDefinition();
            }

            return type1 == type2;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(GetTypeHashCode(DeclaringType));
            hash.Add(MemberKind);
            hash.Add(Name);

            if (MemberKind == MemberType.Method)
            {
                hash.Add(GetTypeHashCode(ReturnType));
                if (Parameters is not null)
                {
                    foreach (var param in Parameters)
                    {
                        hash.Add(GetTypeHashCode(param));
                    }
                }
            }

            return hash.ToHashCode();
        }

        private static int GetTypeHashCode(Type? type)
        {
            if (type == null) return 0;
            return type.IsGenericType ? type.GetGenericTypeDefinition().GetHashCode() : type.GetHashCode();
        }

        public static MemberKey FromMethodInfo(MethodInfo method)
        {
            return new MemberKey(
                method.DeclaringType,
                MemberType.Method,
                method.Name,
                method.ReturnType.IsGenericParameter ? typeof(object) : method.ReturnType,
                method.GetParameters().Select(p => p.ParameterType.IsGenericParameter ? typeof(object) : p.ParameterType).ToArray());
        }

        public static MemberKey FromPropertyInfo(PropertyInfo property)
        {
            return new MemberKey(
                property.DeclaringType,
                MemberType.Property,
                property.Name,
                null,
                null);
        }

        public static MemberKey FromTypeInfo(Type type)
        {
            return new MemberKey(
                type,
                MemberType.Type,
                null,
                null,
                null);
        }
    }

    file enum MemberType
    {
        Type,
        Property,
        Method
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file static class XmlCommentCache
    {
        private static Dictionary<MemberKey, XmlComment>? _cache;
        public static Dictionary<MemberKey, XmlComment> Cache => _cache ??= GenerateCacheEntries();

        private static Dictionary<MemberKey, XmlComment> GenerateCacheEntries()
        {
            var _cache = new Dictionary<MemberKey, XmlComment>();

            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Type, null, null, []), new XmlComment(@"Every class and member should have a one sentence
summary describing its purpose.", null, @"     You can expand on that one sentence summary to
     provide more information for readers. In this case,
     the `ExampleClass` provides different C#
     elements to show how you would add documentation
    comments for most elements in a typical class.
     The remarks can add multiple paragraphs, so you can
write detailed information for developers that use
your work. You should add everything needed for
readers to be successful. This class contains
examples for the following:
     * Summary

This should provide a one sentence summary of the class or member.
* Remarks

This is typically a more detailed description of the class or member
* para

The para tag separates a section into multiple paragraphs
* list

Provides a list of terms or elements
* returns, param

Used to describe parameters and return values
* value
Used to describe properties
* exception

Used to describe exceptions that may be thrown
* c, cref, see, seealso

These provide code style and links to other
documentation elements
* example, code

These are used for code examples
     The list above uses the ""table"" style. You could
also use the ""bullet"" or ""number"" style. Neither
would typically use the ""term"" element.

Note: paragraphs are double spaced. Use the *br*
tag for single spaced lines.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::Person), MemberType.Type, null, null, []), new XmlComment(@"This is an example of a positional record.", null, @"There isn't a way to add XML comments for properties
created for positional records, yet. The language
design team is still considering what tags should
be supported, and where. Currently, you can use
the ""param"" tag to describe the parameters to the
primary constructor.", null, null, false, null, [new XmlParameterComment(@"FirstName", @"This tag will apply to the primary constructor parameter.", null, false), new XmlParameterComment(@"LastName", @"This tag will apply to the primary constructor parameter.", null, false)], null));
            _cache.Add(new MemberKey(typeof(global::MainClass), MemberType.Type, null, null, []), new XmlComment(@"A summary about this class.", null, @"These remarks would explain more about this class.
In this example, these comments also explain the
general information about the derived class.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::DerivedClass), MemberType.Type, null, null, []), new XmlComment(@"A summary about this class.", null, @"These remarks would explain more about this class.
In this example, these comments also explain the
general information about the derived class.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ITestInterface), MemberType.Type, null, null, []), new XmlComment(@"This interface would describe all the methods in
its contract.", null, @"While elided for brevity, each method or property
in this interface would contain docs that you want
to duplicate in each implementing class.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ImplementingClass), MemberType.Type, null, null, []), new XmlComment(@"This interface would describe all the methods in
its contract.", null, @"While elided for brevity, each method or property
in this interface would contain docs that you want
to duplicate in each implementing class.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::InheritOnlyReturns), MemberType.Type, null, null, []), new XmlComment(@"This class shows hows you can ""inherit"" the doc
comments from one method in another method.", null, @"You can inherit all comments, or only a specific tag,
represented by an xpath expression.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::InheritAllButRemarks), MemberType.Type, null, null, []), new XmlComment(@"This class shows an example of sharing comments across methods.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::GenericClass<>), MemberType.Type, null, null, []), new XmlComment(@"This is a generic class.", null, @"This example shows how to specify the GenericClass&lt;T&gt;
type as a cref attribute.
In generic classes and methods, you'll often want to reference the
generic type, or the type parameter.", null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ParamsAndParamRefs), MemberType.Type, null, null, []), new XmlComment(@"This shows examples of typeparamref and typeparam tags", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Property, "Label", null, []), new XmlComment(null, null, @"    The string? ExampleClass.Label is a <see langword=""string"" />
    that you use for a label.
    Note that there isn't a way to provide a ""cref"" to
each accessor, only to the property itself.", null, @"The `Label` property represents a label
for this instance.", false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::Person), MemberType.Property, "FirstName", null, []), new XmlComment(@"This tag will apply to the primary constructor parameter.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::Person), MemberType.Property, "LastName", null, []), new XmlComment(@"This tag will apply to the primary constructor parameter.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Method, "Add", typeof(global::System.Int32), [typeof(global::System.Int32), typeof(global::System.Int32)]), new XmlComment(@"Adds two integers and returns the result.", null, null, @"The sum of two integers.", null, false, [@"    ```int c = Math.Add(4, 5);
if (c &gt; 10)
{
    Console.WriteLine(c);
}```"], [new XmlParameterComment(@"left", @"The left operand of the addition.", null, false), new XmlParameterComment(@"right", @"The right operand of the addition.", null, false)], null));
            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Method, "AddAsync", typeof(global::System.Threading.Tasks.Task<>), [typeof(global::System.Int32), typeof(global::System.Int32)]), new XmlComment(@"This method is an example of a method that
returns an awaitable item.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Method, "DoNothingAsync", typeof(global::System.Threading.Tasks.Task), []), new XmlComment(@"This method is an example of a method that
returns a Task which should map to a void return type.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ExampleClass), MemberType.Method, "AddNumbers", typeof(global::System.Int32), [typeof(global::System.Int32[])]), new XmlComment(@"This method is an example of a method that consumes
an params array.", null, null, null, null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ITestInterface), MemberType.Method, "Method", typeof(global::System.Int32), [typeof(global::System.Int32)]), new XmlComment(@"This method is part of the test interface.", null, @"This content would be inherited by classes
that implement this interface when the
implementing class uses ""inheritdoc""", @"The value of arg", null, false, null, [new XmlParameterComment(@"arg", @"The argument to the method", null, false)], null));
            _cache.Add(new MemberKey(typeof(global::InheritOnlyReturns), MemberType.Method, "MyParentMethod", typeof(global::System.Boolean), [typeof(global::System.Boolean)]), new XmlComment(@"In this example, this summary is only visible for this method.", null, null, @"A boolean", null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::InheritOnlyReturns), MemberType.Method, "MyChildMethod", typeof(global::System.Boolean), []), new XmlComment(null, null, null, @"A boolean", null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::InheritAllButRemarks), MemberType.Method, "MyParentMethod", typeof(global::System.Boolean), [typeof(global::System.Boolean)]), new XmlComment(@"In this example, this summary is visible on all the methods.", null, @"The remarks can be inherited by other methods
using the xpath expression.", @"A boolean", null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::InheritAllButRemarks), MemberType.Method, "MyChildMethod", typeof(global::System.Boolean), []), new XmlComment(@"In this example, this summary is visible on all the methods.", null, null, @"A boolean", null, false, null, null, null));
            _cache.Add(new MemberKey(typeof(global::ParamsAndParamRefs), MemberType.Method, "GetGenericValue", typeof(object), [typeof(object)]), new XmlComment(@"The GetGenericValue method.", null, @"This sample shows how to specify the T ParamsAndParamRefs.GetGenericValue&lt;T&gt;(T para)
method as a cref attribute.
The parameter and return value are both of an arbitrary type,
T", null, null, false, null, null, null));

            return _cache;
        }

        internal static bool TryGetXmlComment(Type? type, MethodInfo? methodInfo, [NotNullWhen(true)] out XmlComment? xmlComment)
        {
            if (methodInfo is null)
            {
                return Cache.TryGetValue(new MemberKey(type, MemberType.Property, null, null, null), out xmlComment);
            }

            return Cache.TryGetValue(MemberKey.FromMethodInfo(methodInfo), out xmlComment);
        }

        internal static bool TryGetXmlComment(Type? type, string? memberName, [NotNullWhen(true)] out XmlComment? xmlComment)
        {
            return Cache.TryGetValue(new MemberKey(type, memberName is null ? MemberType.Type : MemberType.Property, memberName, null, null), out xmlComment);
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file class XmlCommentOperationTransformer : IOpenApiOperationTransformer
    {
        public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
        {
            var methodInfo = context.Description.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor
                ? controllerActionDescriptor.MethodInfo
                : context.Description.ActionDescriptor.EndpointMetadata.OfType<MethodInfo>().SingleOrDefault();

            if (methodInfo is null)
            {
                return Task.CompletedTask;
            }
            if (XmlCommentCache.TryGetXmlComment(methodInfo.DeclaringType, methodInfo, out var methodComment))
            {
                if (methodComment.Summary is { } summary)
                {
                    operation.Summary = summary;
                }
                if (methodComment.Description is { } description)
                {
                    operation.Description = description;
                }
                if (methodComment.Remarks is { } remarks)
                {
                    operation.Description = remarks;
                }
                if (methodComment.Parameters is { Count: > 0})
                {
                    foreach (var parameterComment in methodComment.Parameters)
                    {
                        var parameterInfo = methodInfo.GetParameters().SingleOrDefault(info => info.Name == parameterComment.Name);
                        var operationParameter = operation.Parameters?.SingleOrDefault(parameter => parameter.Name == parameterComment.Name);
                        if (operationParameter is not null)
                        {
                            var targetOperationParameter = operationParameter is OpenApiParameterReference reference
                                ? reference.Target
                                : (OpenApiParameter)operationParameter;
                            targetOperationParameter.Description = parameterComment.Description;
                            if (parameterComment.Example is { } jsonString)
                            {
                                targetOperationParameter.Example = jsonString.Parse();
                            }
                            targetOperationParameter.Deprecated = parameterComment.Deprecated;
                        }
                        else
                        {
                            var requestBody = operation.RequestBody;
                            if (requestBody is not null)
                            {
                                requestBody.Description = parameterComment.Description;
                                if (parameterComment.Example is { } jsonString)
                                {
                                    foreach (var mediaType in requestBody.Content.Values)
                                    {
                                        mediaType.Example = jsonString.Parse();
                                    }
                                }
                            }
                        }
                    }
                }
                if (methodComment.Responses is { Count: > 0} && operation.Responses is { Count: > 0 })
                {
                    foreach (var response in operation.Responses)
                    {
                        var responseComment = methodComment.Responses.SingleOrDefault(xmlResponse => xmlResponse.Code == response.Key);
                        if (responseComment is not null)
                        {
                            response.Value.Description = responseComment.Description;
                        }
                    }
                }
            }

            return Task.CompletedTask;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file class XmlCommentSchemaTransformer : IOpenApiSchemaTransformer
    {
        public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        {
            if (context.JsonPropertyInfo is { AttributeProvider: PropertyInfo propertyInfo })
            {
                if (XmlCommentCache.TryGetXmlComment(propertyInfo.DeclaringType, propertyInfo.Name, out var propertyComment))
                {
                    schema.Description = propertyComment.Value ?? propertyComment.Returns ?? propertyComment.Summary;
                    if (propertyComment.Examples?.FirstOrDefault() is { } jsonString)
                    {
                        schema.Example = jsonString.Parse();
                    }
                }
            }
            if (XmlCommentCache.TryGetXmlComment(context.JsonTypeInfo.Type, (string?)null, out var typeComment))
            {
                schema.Description = typeComment.Summary;
                if (typeComment.Examples?.FirstOrDefault() is { } jsonString)
                {
                    schema.Example = jsonString.Parse();
                }
            }
            return Task.CompletedTask;
        }
    }

    file static class JsonNodeExtensions
    {
        public static JsonNode? Parse(this string? json)
        {
            if (json is null)
            {
                return null;
            }

            try
            {
                return JsonNode.Parse(json);
            }
            catch (JsonException)
            {
                try
                {
                    // If parsing fails, try wrapping in quotes to make it a valid JSON string
                    return JsonNode.Parse($"\"{json.Replace("\"", "\\\"")}\"");
                }
                catch (JsonException)
                {
                    return null;
                }
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.AspNetCore.OpenApi.SourceGenerators, Version=42.42.42.42, Culture=neutral, PublicKeyToken=adb9793829ddae60", "42.42.42.42")]
    file static class GeneratedServiceCollectionExtensions
    {
        [InterceptsLocation]
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            return services.AddOpenApi("v1", options =>
            {
                options.AddSchemaTransformer(new XmlCommentSchemaTransformer());
                options.AddOperationTransformer(new XmlCommentOperationTransformer());
            });
        }

    }
}