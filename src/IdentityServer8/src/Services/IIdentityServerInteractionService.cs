/*
 Copyright (c) 2024 HigginsSoft
 Written by Alexander Higgins https://github.com/alexhiggins732/ 
 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code for this software can be found at https://github.com/alexhiggins732/IdentityServer8

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

*/

using IdentityServer8.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer8.Services;

/// <summary>
///  Provide services be used by the user interface to communicate with IdentityServer.
/// </summary>
public interface IIdentityServerInteractionService
{
    /// <summary>
    /// Gets the authorization context.
    /// </summary>
    /// <param name="returnUrl">The return URL.</param>
    Task<AuthorizationRequest> GetAuthorizationContextAsync(string returnUrl);

    /// <summary>
    /// Indicates if the returnUrl is a valid URL for redirect after login or consent.
    /// </summary>
    /// <param name="returnUrl">The return URL.</param>
    bool IsValidReturnUrl(string returnUrl);

    /// <summary>
    /// Gets the error context.
    /// </summary>
    /// <param name="errorId">The error identifier.</param>
    Task<ErrorMessage> GetErrorContextAsync(string errorId);

    /// <summary>
    /// Gets the logout context.
    /// </summary>
    /// <param name="logoutId">The logout identifier.</param>
    Task<LogoutRequest> GetLogoutContextAsync(string logoutId);

    /// <summary>
    /// Used to create a logoutId if there is not one presently.
    /// </summary>
    /// <returns></returns>
    Task<string> CreateLogoutContextAsync();

    /// <summary>
    /// Informs IdentityServer of the user's consent.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="consent">The consent.</param>
    /// <param name="subject">The subject.</param>
    Task GrantConsentAsync(AuthorizationRequest request, ConsentResponse consent, string subject = null);

    /// <summary>
    /// Triggers error back to the client for the authorization request.
    /// This API is a simpler helper on top of GrantConsentAsync.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="error"></param>
    /// <param name="errorDescription"></param>
    Task DenyAuthorizationAsync(AuthorizationRequest request, AuthorizationError error, string errorDescription = null);

    /// <summary>
    /// Returns a collection representing all of the user's consents and grants.
    /// </summary>
    Task<IEnumerable<Grant>> GetAllUserGrantsAsync();

    /// <summary>
    /// Revokes all a user's consents and grants for a client.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    Task RevokeUserConsentAsync(string clientId);

    /// <summary>
    /// Revokes all of a user's consents and grants for clients the user has signed into during their current session.
    /// </summary>
    Task RevokeTokensForCurrentSessionAsync();
}
