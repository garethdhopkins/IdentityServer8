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

namespace IdentityServerHost.Configuration;

public static class Clients
{
    public static IEnumerable<Client> Get()
    {
        var clients = new List<Client>();

        clients.AddRange(ClientsConsole.Get());
        clients.AddRange(ClientsWeb.Get());

        return clients;
    }
}
