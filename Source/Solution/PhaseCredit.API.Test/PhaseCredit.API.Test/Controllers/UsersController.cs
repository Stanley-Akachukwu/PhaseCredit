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
      //  private readonly ITokenService _tokenService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
           // _tokenService = tokenService;
        }
        // [AuthTokenFilterAttribute]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            //var tokenResponse = new TokenResponse();
            var response = new UsersResponse();
            List<User> users = new List<User>();
            var accessToken = "none";
            response = await _userService.GetListAsync(accessToken);
            //   var accessToken = Request.Cookies["jwtCookie"];

            //if (string.IsNullOrEmpty(accessToken))
            //{
            //    tokenResponse = await _tokenService.GetToken(scope: "phaseCreditAPI");
            //    if (!tokenResponse.IsError && !string.IsNullOrEmpty(tokenResponse.AccessToken))
            //    {
            //        SetJWTCookie(tokenResponse.AccessToken);
            //        accessToken = tokenResponse.AccessToken;
            //    }
            //}


            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    response = await _userService.GetListAsync(accessToken);
            //    //if (response.ResponseCode != HttpStatusCode.OK)
            //    //{
            //    //    tokenResponse = await _tokenService.GetToken(scope: "phaseCreditAPI");
            //    //    response = await _userService.GetListAsync(tokenResponse.AccessToken);
            //    //}
            //}

            //if (response.ResponseCode == HttpStatusCode.Unauthorized)
            //{
            //    HttpContext.Session.SetString("message", response.ResponseMessage);
            //    string test = "https://localhost:5443/Account/Login?ReturnUrl=https://localhost:5444/home/privacy&message="+"you are not authorized to access";
            //    return Redirect(test);
            //}
            //if (response.ResponseCode == HttpStatusCode.Forbidden)
            //{
            //    HttpContext.Session.SetString("message", "You are not permitted to the resource.");
            //    string test = "https://localhost:5443/Account/Login?ReturnUrl=https://localhost:5444/home/privacy&message=" + "Access denied!";
            //    return Redirect(test);
            //}
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
