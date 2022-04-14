using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.Core.DTOs.ClientAuthorization;
using SimpleSoft.Mediator;
using System.Net;

namespace PhaseCredit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    //OAuth 2.0 - client authorization
    //Note:OpenID Connect is about who someone is. OAuth 2.0 is about what they are allowed to do.
    public class ClientAuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AccessToken")]
        public async Task<GetAccessTokenResponse> GetAccessTokenAsync([FromBody] GetAccessTokenRequest request, CancellationToken ct)
        {
            var response = new GetAccessTokenResponse();

            if (string.IsNullOrEmpty(request.ClientId) || string.IsNullOrEmpty(request.ClientSecret))
            {
                response.ResponseCode = HttpStatusCode.BadRequest;
                return response;
            }
            response = await _mediator.SendAsync(new AuthorizeClientCommand
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
            }, ct);

            return response;
        }
    }
}
