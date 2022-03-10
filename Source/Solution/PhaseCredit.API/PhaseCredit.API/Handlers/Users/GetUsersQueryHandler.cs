using PhaseCredit.API.Queries.Users;
using PhaseCredit.Core.DTOs.Users;
using PhaseCredit.Data.Abstract;
using SimpleSoft.Mediator;

namespace PhaseCredit.API.Handlers.Users
{
    public class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, UsersResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IMediator mediator, IUserRepository userRepository)
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
                    ResponseCode = StatusCodes.Status404NotFound,
                    ResponseMessage = "Users list not found"
                };
            }

            return new UsersResponse
            {
                Users = users.ToList(),
                ResponseCode = StatusCodes.Status200OK,
                ResponseMessage = "Successful"
            };
        }
    }
}
