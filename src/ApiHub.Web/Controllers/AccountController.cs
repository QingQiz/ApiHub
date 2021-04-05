using System;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace ApiHub.Web.Controllers
{
    public class AccountController : AbpController
    {
        public const string GithubAuthUri = "https://github.com/login/oauth/authorize";
        public const string GithubTokenUri = "https://github.com/login/oauth/access_token";
        private const string GithubCurrentUserUri = "https://api.github.com/user";
        private readonly string _githubClientId;
        private readonly string _githubClientSecret;

        private IdentityUserManager _userManager;
        private Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController _accountController;
        private SignInManager<IdentityUser> _signInManager;
            

        public AccountController(IConfiguration configuration, IdentityUserManager userManager, Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController accountController, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _accountController = accountController;
            _signInManager = signInManager;
            var configuration1 = configuration;
            _githubClientId = configuration1["AuthServer:OAuthClientId"];
            _githubClientSecret = configuration1["AuthServer:OAuthClientSecret"];
        }

        [HttpGet, Route("/Account/Login/Github")]
        public async Task<ActionResult> LoginViaGithub(string code, string returnUrl)
        {
            if (code.IsNullOrWhiteSpace())
            {
                return Redirect(GithubAuthUri.SetQueryParams(new
                {
                    client_id = _githubClientId,
                    redirect_uri = Url.ActionLink(nameof(LoginViaGithub)),
                    allown_signup = false
                }));
            }

            dynamic authedUserInfo;

            try
            {
                var postAction = await GithubTokenUri.PostJsonAsync(new
                {
                    client_id = _githubClientId,
                    client_secret = _githubClientSecret,
                    code,
                });

                var token = (await postAction.GetStringAsync()).Split('&')[0].Split('=')[1];

                authedUserInfo = await GithubCurrentUserUri
                    .WithHeaders(new
                    {
                        User_Agent = "FlUrl",  // github api requires a user-agent
                        Accept = "application/vnd.github.v3+json",
                        Authorization = "token " + token
                    }).GetJsonAsync();
            }
            catch (FlurlHttpException)
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
                // return RedirectToAction(nameof(LoginViaGithub), new { returnUrl });
            }

            long userId = authedUserInfo.id;

            if (!ApiHubConsts.AdminGithubAccountId.Contains(userId))
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var user = await _userManager.FindByNameAsync(userId.ToString());
            
            if (user == null)
            {
                var newUser = new IdentityUser(Guid.NewGuid(), $"{userId}", $"{userId}@temp.mail.com");
                
                await _userManager.CreateAsync(newUser);
                user = await _userManager.FindByNameAsync(userId.ToString());
            }

            await _signInManager.SignInAsync(user, true);

            return Redirect(returnUrl.IsNullOrEmpty() ? "/" : returnUrl);
        }
    }
}