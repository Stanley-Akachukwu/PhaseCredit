using Microsoft.IdentityModel.Tokens;
using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.Core.BusinessLogic.Authentication;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.Services.Users;
using PhaseCredit.Data.Entities.Users;
using SimpleSoft.Mediator;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhaseCredit.API.Handlers.Authentications
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserLoginResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationManager _authManger;
        public LoginCommandHandler(IUserService userService, IAuthenticationManager authManger)
        {
            _userService = userService;
            _authManger = authManger;
        }
         

        public async Task<UserLoginResponse> HandleAsync(LoginCommand cmd, CancellationToken ct)
        {
            var response = new UserLoginResponse();
            var erroMessages = new List<string>();  
            if (cmd == null)
            {
                response?.ErrorMessages?.Add("Invalid login details!");
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = StatusCodes.Status404NotFound;
                return response;
            }

            User loginUser = await _userService.FindUserAsync(cmd.UserName);

            if (loginUser == null)
            {
                erroMessages.Add($"User was not found for {cmd.UserName}");
                response.ErrorMessages=erroMessages;
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = StatusCodes.Status404NotFound;
                return response;
            }
            var claims = new[] { new Claim(ClaimTypes.Role, loginUser.Role) };
            var accessToken = await _authManger.GenerateJSONWebToken(claims);

            if (accessToken == null)
            {
                erroMessages.Add($"Error generating access token!");
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = StatusCodes.Status500InternalServerError;
                response.ErrorMessages= erroMessages;
                return response;
            }


            if (accessToken != null)
            {
                response.ResponseMessage = "Succcessful";
                response.ResponseCode = StatusCodes.Status201Created;
                response.AccessToken = accessToken;
                response.Id = Guid.NewGuid();
            }
            return response;
        }
       
    }
}
 