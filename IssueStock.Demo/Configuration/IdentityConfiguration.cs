using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IssueStock.Demo.Configuration
{
    /// <summary>
    /// NOTE : Configuration settings for testing purposes. 
    /// In real life scenario its recommend to use Entity Framework and ASP.NET Core Identity to manage users over a database
    /// </summary>
    public class IdentityConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("stockApi")
                {
                    Scopes = new List<string>{ "stockApi.read", "stockApi.write" },
                    ApiSecrets = new List<Secret>{ new Secret("stockapisecret".Sha256()) }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("stockApi.read"),
                new ApiScope("stockApi.write"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "stockApi.client",
                    ClientName = "Stock Client Credentials",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "stockApi.read", "stockApi.write" }
                },
            };

        public static List<TestUser> TestUsers => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "10001",
                Username = "test_admin",
                Password = "test123",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Test Admin"),
                    new Claim(JwtClaimTypes.GivenName, "Test"),
                    new Claim(JwtClaimTypes.FamilyName, "Admin"),
                    new Claim(JwtClaimTypes.WebSite, "https://google.com"),
                }
            }
        };
    }
}
