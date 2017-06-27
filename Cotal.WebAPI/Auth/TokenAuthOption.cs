using System;
using Microsoft.IdentityModel.Tokens;

namespace Cotal.WebAPI.Auth
{
    public class TokenAuthOption
    {
        public string Path { get;  } = "/token";

        public static string Issuer { get; } = "ExampleIssuer";

        public static string Audience { get;  } = "ExampleAudience";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RsaKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get;  } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(40);
        public static string TokenType { get; } = "Bearer";
    }
}