using PhaseCredit.API.Queries.Users;
using PhaseCredit.Core.DTOs.Users;
using PhaseCredit.Core.Services.Users;
using SimpleSoft.Mediator;
using System.Net;

namespace PhaseCredit.API.Handlers.Users
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, UsersResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userRepository;

        public GetUsersQueryHandler(IMediator mediator, IUserService userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }
        public async Task<UsersResponse> HandleAsync(GetUsersQuery query, CancellationToken ct)
        {
            var users = await _userRepository.GetUsersAsync();
            if (users == null)
            {
                return new UsersResponse
                {
                    ResponseCode = HttpStatusCode.NotFound,
                    ResponseMessage = "Users list not found"
                };
            }

            return new UsersResponse
            {
                Users = users.ToList(),
                ResponseCode = HttpStatusCode.OK,
                ResponseMessage = "Successful"
            };
        }
    }
}
