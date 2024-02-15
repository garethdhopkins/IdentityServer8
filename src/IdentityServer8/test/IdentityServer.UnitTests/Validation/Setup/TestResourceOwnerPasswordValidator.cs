/*
 Copyright (c) 2024 HigginsSoft
 Written by Alexander Higgins https://github.com/alexhiggins732/ 
 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code for this software can be found at https://github.com/alexhiggins732/IdentityServer8

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

*/

using System.Threading.Tasks;
using IdentityServer8.Models;
using IdentityServer8.Validation;

namespace IdentityServer.UnitTests.Validation.Setup
{
    public class TestResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private string _erroDescription;
        private TokenRequestErrors _error;
        private readonly bool _sendError;

        public TestResourceOwnerPasswordValidator()
        { }

        public TestResourceOwnerPasswordValidator(TokenRequestErrors error, string errorDescription = null)
        {
            _sendError = true;
            _error = error;
            _erroDescription = errorDescription;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_sendError)
            {
                context.Result = new GrantValidationResult(_error, _erroDescription);
                return Task.CompletedTask;
            }

            if (context.UserName == context.Password)
            {
                context.Result = new GrantValidationResult(context.UserName, "password");
            }
            
            if (context.UserName == "bob_no_password" && context.Password == "")
            {
                context.Result = new GrantValidationResult(context.UserName, "password");
            }

            return Task.CompletedTask;
        }
    }
}
