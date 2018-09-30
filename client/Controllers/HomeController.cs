using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();

        [Authorize]
        public IActionResult Login() => RedirectToAction("Index");

        public async Task<IActionResult> GetUsingClientCredentials()
        {
            var client = new HttpClient();
            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5000/connect/token",
                GrantType = OidcConstants.GrantTypes.ClientCredentials,
                Scope = "api1",

                ClientAssertion = new ClientAssertion
                {
                    Type = OidcConstants.ClientAssertionTypes.JwtBearer,
                    Value = TokenGenerator.CreateClientAuthJwt()
                }
            });

            return View(response);
        }
    }
}
