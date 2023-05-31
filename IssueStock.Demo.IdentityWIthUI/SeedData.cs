// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using IssueStock.Demo.IdentityWIthUI.Data;
using IssueStock.Demo.IdentityWIthUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IssueStock.Demo.IdentityWIthUI
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    // add roles
                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var admin = roleMgr.FindByNameAsync("Admin").Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole
                        {
                            Name = "Admin"
                        };
                        _ = roleMgr.CreateAsync(admin).Result;
                    }

                    var user = roleMgr.FindByNameAsync("User").Result;
                    if (user == null)
                    {
                        user = new IdentityRole
                        {
                            Name = "User"
                        };
                        _ = roleMgr.CreateAsync(user).Result;
                    }

                    var auditor = roleMgr.FindByNameAsync("Auditor").Result;
                    if (auditor == null)
                    {
                        auditor = new IdentityRole
                        {
                            Name = "Auditor"
                        };
                        _ = roleMgr.CreateAsync(auditor).Result;
                    }

                    // add users
                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new ApplicationUser
                        {
                            UserName = "alice",
                            Email = "AliceSmith@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(alice, user.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(alice, user.Name).Result;
                        }

                        Log.Debug("alice created");
                    }
                    else
                    {
                        Log.Debug("alice already exists");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new ApplicationUser
                        {
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(bob, admin.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(bob, admin.Name).Result;
                        }

                        Log.Debug("bob admin created");
                    }
                    else
                    {
                        Log.Debug("bob admin already exists");
                    }

                    var danny = userMgr.FindByNameAsync("danny").Result;
                    if (danny == null)
                    {
                        danny = new ApplicationUser
                        {
                            UserName = "danny",
                            Email = "DannySmith@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(danny, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(danny, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Danny Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Danny"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://danny.com"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(danny, auditor.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(danny, auditor.Name).Result;
                        }

                        Log.Debug("danny auditor created");
                    }
                    else
                    {
                        Log.Debug("danny auditor already exists");
                    }
                }
            }
        }
    }
}
