// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.JSInterop.Infrastructure;
using static Microsoft.AspNetCore.Internal.LinkerFlags;

namespace Microsoft.JSInterop;

/// <summary>
/// Abstract base class for an in-process JavaScript runtime.
/// </summary>
public abstract class JSInProcessRuntime : JSRuntime, IJSInProcessRuntime
{
    /// <summary>
    /// Invokes the specified JavaScript function synchronously.
    /// </summary>
    /// <typeparam name="TValue">The JSON-serializable return type.</typeparam>
    /// <param name="identifier">An identifier for the function to invoke. For example, the value <c>"someScope.someFunction"</c> will invoke the function <c>window.someScope.someFunction</c>.</param>
    /// <param name="args">JSON-serializable arguments.</param>
    /// <returns>An instance of <typeparamref name="TValue"/> obtained by JSON-deserializing the return value.</returns>
    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public TValue Invoke<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(string identifier, params object?[]? args)
        => Invoke<TValue>(identifier, 0, JSCallType.FunctionCall, args);

    /// <inheritdoc />
    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public IJSObjectReference InvokeNew(string identifier, object?[]? args)
        => Invoke<IJSObjectReference>(identifier, 0, JSCallType.NewCall, args);

    /// <inheritdoc />
    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public TValue GetValue<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(string identifier)
        => Invoke<TValue>(identifier, 0, JSCallType.GetValue);

    /// <inheritdoc />
    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    public void SetValue<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(string identifier, TValue value)
        => Invoke<IJSVoidResult>(identifier, 0, JSCallType.SetValue, value);

    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed.")]
    internal TValue Invoke<[DynamicallyAccessedMembers(JsonSerialized)] TValue>(string identifier, long targetInstanceId, JSCallType callType, params object?[]? args)
    {
        var argsJson = args is not null && args.Length != 0 ? JsonSerializer.Serialize(args, JsonSerializerOptions) : "[]";
        var resultType = JSCallResultTypeHelper.FromGeneric<TValue>();
        var invocationInfo = new JSInvocationInfo
        {
            AsyncHandle = 0,
            TargetInstanceId = targetInstanceId,
            Identifier = identifier,
            CallType = callType,
            ResultType = resultType,
            ArgsJson = argsJson,
        };

        var resultJson = InvokeJS(invocationInfo);

        // While the result of deserialization could be null, we're making a
        // quality of life decision and letting users explicitly determine if they expect
        // null by specifying TValue? as the expected return type.
        if (resultJson is null)
        {
            return default!;
        }

        var result = JsonSerializer.Deserialize<TValue>(resultJson, JsonSerializerOptions)!;
        ByteArraysToBeRevived.Clear();
        return result;
    }

    /// <summary>
    /// Performs a synchronous function invocation.
    /// </summary>
    /// <param name="invocationInfo">Configuration of the interop call.</param>
    /// <returns>A JSON representation of the result.</returns>
    protected abstract string? InvokeJS(JSInvocationInfo invocationInfo);

    /// <summary>
    /// Performs a synchronous function invocation.
    /// </summary>
    /// <param name="identifier">The identifier for the function to invoke.</param>
    /// <param name="argsJson">A JSON representation of the arguments.</param>
    /// <returns>A JSON representation of the result.</returns>
    protected virtual string? InvokeJS(string identifier, [StringSyntax(StringSyntaxAttribute.Json)] string? argsJson)
        => InvokeJS(new JSInvocationInfo
        {
            AsyncHandle = 0,
            TargetInstanceId = 0,
            Identifier = identifier,
            CallType = JSCallType.FunctionCall,
            ResultType = JSCallResultType.Default,
            ArgsJson = argsJson,
        });
}
