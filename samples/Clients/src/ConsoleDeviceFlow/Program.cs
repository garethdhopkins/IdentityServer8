/*
 Copyright (c) 2024 HigginsSoft
 Written by Alexander Higgins https://github.com/alexhiggins732/ 
 

 Copyright (c) 2018, Brock Allen & Dominick Baier. All rights reserved.

 Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information. 
 Source code for this software can be found at https://github.com/alexhiggins732/IdentityServer8

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

*/

using Clients;
using IdentityModel;
using IdentityModel.Client;
using System.Diagnostics;
using System.Text.Json;

namespace ConsoleDeviceFlow
{
    public class Program
    {
        static IDiscoveryCache _cache = new DiscoveryCache(Constants.Authority);

        public static async Task Main()
        {
            Console.Title = "Console Device Flow";

            var authorizeResponse = await RequestAuthorizationAsync();

            var tokenResponse = await RequestTokenAsync(authorizeResponse);
            tokenResponse.Show();

            Console.ReadLine();
            await CallServiceAsync(tokenResponse.AccessToken);
        }

        static async Task<DeviceAuthorizationResponse> RequestAuthorizationAsync()
        {
            var disco = await _cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var client = new HttpClient();
            var response = await client.RequestDeviceAuthorizationAsync(new DeviceAuthorizationRequest
            {
                Address = disco.DeviceAuthorizationEndpoint,
                ClientId = "device"
            });

            if (response.IsError) throw new Exception(response.Error);

            Console.WriteLine($"user code   : {response.UserCode}");
            Console.WriteLine($"device code : {response.DeviceCode}");
            Console.WriteLine($"URL         : {response.VerificationUri}");
            Console.WriteLine($"Complete URL: {response.VerificationUriComplete}");

            Console.WriteLine($"\nPress enter to launch browser ({response.VerificationUri})");
            Console.ReadLine();

            Process.Start(new ProcessStartInfo(response.VerificationUriComplete) { UseShellExecute = true });
            return response;
        }

        private static async Task<TokenResponse> RequestTokenAsync(DeviceAuthorizationResponse authorizeResponse)
        {
            var disco = await _cache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var client = new HttpClient();

            while (true)
            {
                var response = await client.RequestDeviceTokenAsync(new DeviceTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "device",
                    DeviceCode = authorizeResponse.DeviceCode
                });

                if (response.IsError)
                {
                    if (response.Error == OidcConstants.TokenErrors.AuthorizationPending || response.Error == OidcConstants.TokenErrors.SlowDown)
                    {
                        Console.WriteLine($"{response.Error}...waiting.");
                        Thread.Sleep(authorizeResponse.Interval * 1000);
                    }
                    else
                    {
                        throw new Exception(response.Error);
                    }
                }
                else
                {
                    return response;
                }
            }
        }

        static async Task CallServiceAsync(string token)
        {
            var baseAddress = Constants.SampleApi;

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var response = await client.GetStringAsync("identity");

            "\n\nService claims:".ConsoleGreen();
            var json = JsonSerializer.Deserialize<JsonElement>(response);
            Console.WriteLine(json);
        }
    }
}
