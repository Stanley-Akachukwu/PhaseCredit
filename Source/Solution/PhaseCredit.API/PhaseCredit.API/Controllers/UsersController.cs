using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhaseCredit.API.Queries.Users;
using PhaseCredit.Core.DTOs.Users;
using SimpleSoft.Mediator;

namespace PhaseCredit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            response = await _mediator.FetchAsync(new GetUsersResponseQuery(), ct);
            return response;
        }
    }
}
