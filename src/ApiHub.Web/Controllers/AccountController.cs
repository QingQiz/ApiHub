using System;
using System.Threading.Tasks;
using ApiHub.ApplicationAttribute;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc;

namespace ApiHub.Web.Controllers
{
    public class AccountController : AbpController
    {
        private const string GithubAuthUri = "https://github.com/login/oauth/authorize";
        private const string GithubTokenUri = "https://github.com/login/oauth/access_token";
        private readonly string _githubClientId;
        private readonly string _githubClientSecret;
            

        public AccountController(IConfiguration configuration)
        {
            var configuration1 = configuration;
            _githubClientId = configuration1["AuthServer:OAuthClientId"];
            _githubClientSecret = configuration1["AuthServer:OAuthClientSecret"];
        }

        [HttpGet, Route("/Api/Login/Github")]
        public async Task<ActionResult> LoginViaGithub(string code, string returnUrl)
        {
            if (code.IsNullOrWhiteSpace())
            {
                return Redirect(GithubAuthUri.SetQueryParams(new
                {
                    client_id = _githubClientId,
                    redirect_uri = Url.ActionLink("LoginViaGithub"),
                    allown_signup = false
                }));
            }

            var res = await GithubTokenUri.PostJsonAsync(new
            {
                client_id = _githubClientId,
                client_secret = _githubClientSecret,
                code,
            });
            
            Logger.Log(LogLevel.Warning, await res.GetStringAsync());

            throw new NotImplementedException();
        }

        [IncludeInSwagger()]
        public RedirectResult LoginCallback()
        {
            return new("/");
        }
    }
}