﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Collections.Generic;

using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;
namespace StsServerIdentity {
    public class Config {
        public static IEnumerable<IdentityResource> GetIdentityResources () {
            return new List<IdentityResource> {
                new IdentityResources.OpenId (),
                new IdentityResources.Profile (),
                new IdentityResources.Email (),
                new IdentityResource ("dataeventrecordsscope", new [] { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }),
                new IdentityResource ("securedfilesscope", new [] { "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user" })
            };
        }
        public static IEnumerable<ApiResource> GetApiResources () {
            return new [] {
                new ApiResource ("dataEventRecords") {
                        ApiSecrets = {
                                new Secret ("dataEventRecordsSecret".Sha256 ())},
                                Scopes = {
                                new Scope {
                                Name = "dataeventrecords",
                                DisplayName = "Scope for the dataEventRecords ApiResource"
                                }
                                },
                                UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                                },
                                new ApiResource ("securedFiles") {
                                ApiSecrets = {
                                new Secret ("securedFilesSecret".Sha256 ())
                                },
                                Scopes = {
                                new Scope {
                                Name = "securedfiles",
                                DisplayName = "Scope for the securedFiles ApiResource"
                                }
                                },
                                UserClaims = { "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user" }
                                }
            };
        }
        public static IEnumerable<Client> GetClients () {
            // TODO use configs in app
            //var yourConfig = stsConfig["ClientUrl"];
            return new [] {
                new Client {
                    ClientName = "vuejs_code_client",
                        ClientId = "vuejs_code_client",
                        AccessTokenType = AccessTokenType.Reference,
                        // RequireConsent = false,
                        AccessTokenLifetime = 330, // 330 seconds, default 60 minutes
                        IdentityTokenLifetime = 300,
                        RequireClientSecret = false,
                        AllowedGrantTypes = GrantTypes.Code,
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = true,
                        RedirectUris = new List<string> {
                            "http://localhost:44357",
                            "http://localhost:44357/callback.html",
                            "http://localhost:44357/silent-renew.html"
                            },
                            PostLogoutRedirectUris = new List<string> {
                            "http://localhost:44357/",
                            "http://localhost:44357"
                            },
                            AllowedCorsOrigins = new List<string> {
                            "http://localhost:44357"
                            },
                            AllowedScopes = new List<string> {
                            "openid",
                            "dataEventRecords",
                            "dataeventrecordsscope",
                            "role",
                            "profile",
                            "email"
                            }
                            },
                new Client {
                    ClientName = "vuex-oidc-sample",
                    ClientId = "vuex-oidc",
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330, // 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string> {
                    "http://localhost:5002/oidc-popup-callback"
                    },
                    PostLogoutRedirectUris = new List<string> {
                    "http://localhost:5002/post-logout"
                    },
                    AllowedCorsOrigins = new List<string> {
                    "http://localhost:5002"
                    },
                    AllowedScopes = new List<string> {
                    "openid",
                    "dataEventRecords",
                    "dataeventrecordsscope",
                    "role",
                    "profile",
                    "email"
                    }
                    }
            };
        }
    }
}
