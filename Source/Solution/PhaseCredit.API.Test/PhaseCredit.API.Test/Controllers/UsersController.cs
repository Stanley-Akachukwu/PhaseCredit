using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Users;
using PhaseCredit.Data.Entities.Users;
using System.Net;

namespace PhaseCredit.API.Test.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        // [AuthTokenFilterAttribute]
        [Authorize]
        public async Task<IActionResult> List()
        {
            var tokenResponse = new TokenResponse();
            var response = new UsersResponse();
            List<User> users = new List<User>();

            var accessToken = Request.Cookies["jwtCookie"];

            if (string.IsNullOrEmpty(accessToken))
            {
                tokenResponse = await _tokenService.GetToken(scope: "phaseCreditAPI");
                if (!tokenResponse.IsError && !string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    SetJWTCookie(tokenResponse.AccessToken);
                    accessToken = tokenResponse.AccessToken;
                }
            }


            if (!string.IsNullOrEmpty(accessToken))
            {
                response = await _userService.GetListAsync(accessToken);
                if (response.ResponseCode != HttpStatusCode.OK)
                {
                    tokenResponse = await _tokenService.GetToken(scope: "phaseCreditAPI");
                    response = await _userService.GetListAsync(tokenResponse.AccessToken);
                }
            }

            if (response.ResponseCode == HttpStatusCode.Unauthorized)
            {
                HttpContext.Session.SetString("message", response.ResponseMessage);
                return RedirectToAction("Login", "Authentication");
            }
            if (response.ResponseCode == HttpStatusCode.Forbidden)
            {
                HttpContext.Session.SetString("message", "You are not permitted to the resource.");
                return RedirectToAction("Login", "Authentication");
            }
            if (response.ResponseCode == HttpStatusCode.OK)
            {
                users = response.Users;
            }
            return View(users);
        }
        private void SetJWTCookie(string accessToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(3),
            };
            Response.Cookies.Append("jwtCookie", accessToken, cookieOptions);
        }
    }
}
