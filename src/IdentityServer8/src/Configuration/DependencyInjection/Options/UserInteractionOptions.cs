/*
 Copyright (c) 2024 HigginsSoft
 Written by Alexander Higgins https://github.com/alexhiggins732/ 
 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code for this software can be found at https://github.com/alexhiggins732/IdentityServer8

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

*/

using IdentityServer8.Extensions;

namespace IdentityServer8.Configuration;

/// <summary>
/// Options for aspects of the user interface.
/// </summary>
public class UserInteractionOptions
{
    /// <summary>
    /// Gets or sets the login URL. If a local URL, the value must start with a leading slash.
    /// </summary>
    /// <value>
    /// The login URL.
    /// </value>
    public string LoginUrl { get; set; } //= Constants.UIConstants.DefaultRoutePaths.Login.EnsureLeadingSlash();

    /// <summary>
    /// Gets or sets the login return URL parameter.
    /// </summary>
    /// <value>
    /// The login return URL parameter.
    /// </value>
    public string LoginReturnUrlParameter { get; set; } //= Constants.UIConstants.DefaultRoutePathParams.Login;

    /// <summary>
    /// Gets or sets the logout URL. If a local URL, the value must start with a leading slash.
    /// </summary>
    /// <value>
    /// The logout URL.
    /// </value>
    public string LogoutUrl { get; set; } //= Constants.UIConstants.DefaultRoutePaths.Logout.EnsureLeadingSlash();

    /// <summary>
    /// Gets or sets the logout identifier parameter.
    /// </summary>
    /// <value>
    /// The logout identifier parameter.
    /// </value>
    public string LogoutIdParameter { get; set; } = Constants.UIConstants.DefaultRoutePathParams.Logout;

    /// <summary>
    /// Gets or sets the consent URL. If a local URL, the value must start with a leading slash.
    /// </summary>
    /// <value>
    /// The consent URL.
    /// </value>
    public string ConsentUrl { get; set; } = Constants.UIConstants.DefaultRoutePaths.Consent.EnsureLeadingSlash();

    /// <summary>
    /// Gets or sets the consent return URL parameter.
    /// </summary>
    /// <value>
    /// The consent return URL parameter.
    /// </value>
    public string ConsentReturnUrlParameter { get; set; } = Constants.UIConstants.DefaultRoutePathParams.Consent;

    /// <summary>
    /// Gets or sets the error URL. If a local URL, the value must start with a leading slash.
    /// </summary>
    /// <value>
    /// The error URL.
    /// </value>
    public string ErrorUrl { get; set; } = Constants.UIConstants.DefaultRoutePaths.Error.EnsureLeadingSlash();

    /// <summary>
    /// Gets or sets the error identifier parameter.
    /// </summary>
    /// <value>
    /// The error identifier parameter.
    /// </value>
    public string ErrorIdParameter { get; set; } = Constants.UIConstants.DefaultRoutePathParams.Error;

    /// <summary>
    /// Gets or sets the custom redirect return URL parameter.
    /// </summary>
    /// <value>
    /// The custom redirect return URL parameter.
    /// </value>
    public string CustomRedirectReturnUrlParameter { get; set; } = Constants.UIConstants.DefaultRoutePathParams.Custom;

    /// <summary>
    /// Gets or sets the cookie message threshold. This limits how many cookies are created, and older ones will be purged.
    /// </summary>
    /// <value>
    /// The cookie message threshold.
    /// </value>
    public int CookieMessageThreshold { get; set; } = Constants.UIConstants.CookieMessageThreshold;

    /// <summary>
    /// Gets or sets the device verification URL.  If a local URL, the value must start with a leading slash.
    /// </summary>
    /// <value>
    /// The device verification URL.
    /// </value>
    public string DeviceVerificationUrl { get; set; } = Constants.UIConstants.DefaultRoutePaths.DeviceVerification;

    /// <summary>
    /// Gets or sets the device verification user code paramater.
    /// </summary>
    /// <value>
    /// The device verification user code parameter.
    /// </value>
    public string DeviceVerificationUserCodeParameter { get; set; } = Constants.UIConstants.DefaultRoutePathParams.UserCode;
}
