using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityModel;

namespace client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Login() => RedirectToAction("Index");

        public async Task<IActionResult> GetUsingClientCredentials()
        {
            var tokenHandler = new JwtSecurityTokenHandler {TokenLifetimeInMinutes = 5};
            var securityToken = tokenHandler.CreateJwtSecurityToken(
                issuer: "client_using_jwt",
                audience: "http://localhost:5000/connect/token",
                subject: new ClaimsIdentity(new List<Claim> { new Claim("sub", "client_using_jwt") }),
                signingCredentials: new SigningCredentials(new X509SecurityKey(new X509Certificate2("./idsrv3test.pfx", "idsrv3test")), "RS256")
            );
            var token = tokenHandler.WriteToken(securityToken);
            
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = "api1",

                ClientAssertion = new ClientAssertion
                {
                    Type = OidcConstants.ClientAssertionTypes.JwtBearer,
                    Value = token
                }
            });

            return View(response);
        }
    }
}
