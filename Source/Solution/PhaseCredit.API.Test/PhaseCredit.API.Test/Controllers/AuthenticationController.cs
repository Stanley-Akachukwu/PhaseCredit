using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Test.Filters;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Authentications;
using System.Net;

namespace PhaseCredit.API.Test.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Message = HttpContext.Session.GetString("message");
            return View();
        }

        [HttpPost]
        [AuthTokenFilterAttribute]
        public async Task<IActionResult> Login(UserLoginRequest loginRequest)
        {
            var isAuthorized = HttpContext.Request.Headers.TryGetValue("AccessToken", out var accessToken);
            var userLoginResponse = await _authService.LoginAsync(loginRequest, accessToken);
            if (userLoginResponse == null)
            {
                HttpContext.Session.SetString("message", "Login Failed");
                return RedirectToAction("Login");
            }
            if (userLoginResponse.ResponseCode != HttpStatusCode.Created)
            {
                HttpContext.Session.SetString("message", userLoginResponse.ResponseMessage);
                return RedirectToAction("Login");
            }
            if (userLoginResponse.ResponseCode == HttpStatusCode.Created)
            {
                SetJWTCookie(userLoginResponse.UserToken);
            }

            // return RedirectToAction("Login");
            // return RedirectToAction("FlightReservation", "TestReservation");
            return RedirectToAction("Users", "Index");
        }
         
        private void SetJWTCookie(string userToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(3),
            };
            Response.Cookies.Append("jwtCookie", userToken, cookieOptions);
        }
    }
}
