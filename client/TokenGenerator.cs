using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace client
{
    public static class TokenGenerator
    {
        public static string CreateClientAuthJwt()
        {
            var tokenHandler = new JwtSecurityTokenHandler { TokenLifetimeInMinutes = 5 };
            var securityToken = tokenHandler.CreateJwtSecurityToken(
                issuer: "client_using_jwt",
                audience: "http://localhost:5000/connect/token",
                subject: new ClaimsIdentity(new List<Claim> { new Claim("sub", "client_using_jwt") }),
                signingCredentials: new SigningCredentials(new X509SecurityKey(new X509Certificate2("./idsrv3test.pfx", "idsrv3test")), "RS256")
            );

            return tokenHandler.WriteToken(securityToken);
        }
    }
}