using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.API.Queries.Users;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.DTOs.Users;
using SimpleSoft.Mediator;
using System.Net;

namespace PhaseCredit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<UsersResponse> GetListAsync(CancellationToken ct)
        {
            //Start from users controller. Look at the sample projects @ C:\Projects++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            var response = new UsersResponse();

            response = await _mediator.FetchAsync(new GetUsersQuery(), ct);
            return response;
        }
         
        [HttpPost("login")]
        public async Task<UserLoginResponse> LoginAsync([FromBody] UserLoginRequest request, CancellationToken ct)
        {
            var response = new UserLoginResponse();

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.UserName))
            {
                response?.ErrorMessages?.Add("Invalid login credentials.");
                response.ResponseMessage = "Failed to login!";
                response.ResponseCode = HttpStatusCode.BadRequest;
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
