using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.API.Common;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.Services.Authentications;
using PhaseCredit.Core.Services.ClientAuthorization;
using PhaseCredit.Core.Services.Users;
using PhaseCredit.Data.Entities.Users;
using SimpleSoft.Mediator;
using System.Net;
using System.Security.Claims;

namespace PhaseCredit.API.Handlers.Authentications
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, UserLoginResponse>
    {
        private readonly IUserService _userService;
        private readonly IClientAuthorization _authManger;
        private readonly IConfiguration _config;
        private readonly IAppSettings _appSettings;

        public LoginCommandHandler(IUserService userService, IClientAuthorization authManger, IConfiguration config, IAppSettings appSettings)
        {
            _userService = userService;
            _authManger = authManger;
            _config = config;
            _appSettings = appSettings; 
        }
         

        public async Task<UserLoginResponse> HandleAsync(LoginCommand cmd, CancellationToken ct)
        {
            var response = new UserLoginResponse();
            var erroMessages = new List<string>();  
            if (cmd == null)
            {
                response?.ErrorMessages?.Add("Invalid login details!");
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }

            User loginUser = await _userService.FindUserAsync(cmd.UserName);

            if (loginUser == null)
            {
                erroMessages.Add($"User was not found for {cmd.UserName}");
                response.ErrorMessages=erroMessages;
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }
            var claims = new[] { new Claim(ClaimTypes.Role, loginUser.Role) };


            response = await _userService.GenerateUserTokenAsync(claims);

            if (response == null)
            {
                erroMessages.Add($"Error generating access token!");
                response.ResponseMessage = "Authentication failed!";
                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages= erroMessages;
                return response;
            }

            if (string.IsNullOrEmpty(response.UserToken) || response.ResponseCode!= HttpStatusCode.Created)
            {
                response.ResponseMessage = "Failed";
                response.ResponseCode = response.ResponseCode;
            }

            if (response.ResponseCode == HttpStatusCode.Created)
            {
                response.ResponseMessage = "Succcessful";
                response.ResponseCode = HttpStatusCode.Created;
                response.Id = Guid.NewGuid();
                response.CreatedDate = DateTime.Now;
            }
            return response;
        }
       
    }
}
 