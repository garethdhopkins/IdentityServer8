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
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace ConsoleEphemeralMtlsClient
{
    class Program
    {
        private static X509Certificate2 ClientCertificate;
        
        static async Task Main(string[] args)
        {
            ClientCertificate = CreateClientCertificate("client");
            
            var response = await RequestTokenAsync();
            response.Show();

            Console.ReadLine();
            await CallServiceAsync(response.AccessToken);
        }
        
        static async Task<TokenResponse> RequestTokenAsync()
        {
            var client = new HttpClient(GetHandler(ClientCertificate));

            var disco = await client.GetDiscoveryDocumentAsync(Constants.AuthorityMtls);
            if (disco.IsError) throw new Exception(disco.Error);

            var endpoint = disco.TokenEndpoint;
                //.TryGetValue(OidcConstants.Discovery.MtlsEndpointAliases)
                //.TryGetValue<string>(OidcConstants.Discovery.TokenEndpoint)
                ////.Value<string>(OidcConstants.Discovery.TokenEndpoint)
                //.ToString();
            
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = endpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "resource1.scope1"
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }

        static async Task CallServiceAsync(string token)
        {
            var client = new HttpClient(GetHandler(ClientCertificate))
            {
                BaseAddress = new Uri(Constants.SampleApiMtls)
            };

            client.SetBearerToken(token);
            var response = await client.GetStringAsync("identity");

            "\n\nService claims:".ConsoleGreen();
            var json = JsonSerializer.Deserialize<JsonElement>(response);
            Console.WriteLine(json);
        }
        
        static X509Certificate2 CreateClientCertificate(string name)
        {
            X500DistinguishedName distinguishedName = new X500DistinguishedName($"CN={name}");

            using (var rsa = RSA.Create(2048))
            {
                var request = new CertificateRequest(distinguishedName, rsa, HashAlgorithmName.SHA256,RSASignaturePadding.Pkcs1);

                request.CertificateExtensions.Add(
                    new X509KeyUsageExtension(X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature , false));

                request.CertificateExtensions.Add(
                    new X509EnhancedKeyUsageExtension(
                        new OidCollection { new Oid("1.3.6.1.5.5.7.3.2") }, false));

                return request.CreateSelfSigned(new DateTimeOffset(DateTime.UtcNow.AddDays(-1)), new DateTimeOffset(DateTime.UtcNow.AddDays(3650)));
            }
        }
        
        static SocketsHttpHandler GetHandler(X509Certificate2 certificate)
        {
            var handler = new SocketsHttpHandler
            {
                SslOptions =
                {
                    ClientCertificates = new X509CertificateCollection {certificate}
                }
            };

            return handler;
        }
    }
}
