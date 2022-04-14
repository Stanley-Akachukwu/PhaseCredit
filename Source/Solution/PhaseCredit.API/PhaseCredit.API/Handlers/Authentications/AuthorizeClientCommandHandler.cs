using PhaseCredit.API.Commands.Authentications;
using PhaseCredit.API.Common;
using PhaseCredit.Core.DTOs.ClientAuthorization;
using PhaseCredit.Core.Services.ClientAuthorization;
using PhaseCredit.Core.Services.Users;
using PhaseCredit.Data.Entities.ClientRegisters;
using SimpleSoft.Mediator;
using System.Net;

namespace PhaseCredit.API.Handlers.Authentications
{
    public class AuthorizeClientCommandHandler : ICommandHandler<AuthorizeClientCommand, GetAccessTokenResponse>
    {
        private readonly IUserService _userService;
        private readonly IClientAuthorization _apiAuthManger;
        private readonly IConfiguration _config;
        private readonly IAppSettings _appSettings;

        public AuthorizeClientCommandHandler(IUserService userService, IClientAuthorization apiAuthManger, IConfiguration config, IAppSettings appSettings)
        {
            _userService = userService;
            _apiAuthManger = apiAuthManger;
            _config = config;
            _appSettings = appSettings; 
        }
         

        public async Task<GetAccessTokenResponse> HandleAsync(AuthorizeClientCommand cmd, CancellationToken ct)
        {
            var response = new GetAccessTokenResponse();
            var erroMessages = new List<string>();  
            if (cmd == null)
            {
                response.Error = $"Invalid client credentials for {cmd.ClientId}";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }

            ClientRegister client = await _apiAuthManger.FindClientRegisterAsync(cmd.ClientId,cmd.ClientSecret);

            if (client == null)
            {
                response.Error = $"{response.Error} {cmd.ClientId}";
                response.ResponseCode = HttpStatusCode.NotFound;
                return response;
            }

            var accessTokenResponse = await _apiAuthManger.GenerateJSONWebToken(cmd.ClientId, cmd.ClientSecret, client.Scope, client.IdentityServer);

            if (accessTokenResponse == null)
            {
                response.Error = accessTokenResponse.Error;
                response.ResponseCode = HttpStatusCode.InternalServerError;
                return response;
            }

            if (!string.IsNullOrEmpty(accessTokenResponse.Error) && accessTokenResponse.ResponseCode!= HttpStatusCode.Created)
            {
                response.Error = accessTokenResponse.Error;
                response.ResponseCode = HttpStatusCode.InternalServerError;
                return response;
            }

            if (accessTokenResponse.ResponseCode == HttpStatusCode.Created)
            {
                response.ResponseCode = HttpStatusCode.Created;
                response.AccessToken = accessTokenResponse.AccessToken;
                response.Id = Guid.NewGuid();
                response.CreatedDate = DateTime.Now;
            }
            return response;
        }
       
    }
}
 