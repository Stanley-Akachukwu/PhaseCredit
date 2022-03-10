using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.Core.DTOs.Authentications;
using SimpleSoft.Mediator;
using System.Security.Claims;

namespace PhaseCredit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<UserLoginResponse> LoginAsync([FromBody] UserLoginRequest request, CancellationToken ct)
        {
            var response = new UserLoginResponse();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.UserName))
            {
                response?.ErrorMessages?.Add("Invalid login credentials.");
                response.ResponseMessage = "Failed to login!";
                response.ResponseCode = StatusCodes.Status400BadRequest;
                return response;
            }
            response = await _mediator.SendAsync(new LoginCommand
            {
                UserName = request.UserName,
               Password = request.Password,
            }, ct);
            
            return response;
        }
        
    }
}
