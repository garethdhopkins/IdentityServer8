/*
 Copyright (c) 2024 HigginsSoft
 Written by Alexander Higgins https://github.com/alexhiggins732/ 
 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code for this software can be found at https://github.com/alexhiggins732/IdentityServer8

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

*/

namespace IdentityServer8.Models;

/// <summary>
/// Request object for device flow interaction
/// </summary>
public class DeviceFlowInteractionResult
{
    /// <summary>
    /// Gets or sets the error description.
    /// </summary>
    /// <value>
    /// The error description.
    /// </value>
    public string ErrorDescription { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this instance is error.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
    /// </value>
    public bool IsError { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is access denied.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is access denied; otherwise, <c>false</c>.
    /// </value>
    public bool IsAccessDenied { get; set; }

    /// <summary>
    /// Create failure result
    /// </summary>
    /// <param name="errorDescription">The error description.</param>
    /// <returns></returns>
    public static DeviceFlowInteractionResult Failure(string errorDescription = null)
    {
        return new DeviceFlowInteractionResult
        {
            IsError = true,
            ErrorDescription = errorDescription
        };
    }
}
