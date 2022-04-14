using IdentityModel.Client;
using PhaseCredit.Core.DTOs.ClientAuthorization;
using PhaseCredit.Core.Services.ClientAuthorization;
using PhaseCredit.Core.Services.Logs;
using PhaseCredit.Data.Entities.ClientRegisters;

namespace PhaseCredit.Core.Services.Authentications
{
    public class ClientAuthorizationService : IClientAuthorization
    {
        private readonly ILogService _logService;
        public ClientAuthorizationService(ILogService logService)
        {
            _logService = logService;
        }

        public async Task<ClientRegister> FindClientRegisterAsync(string clientId,string clientSecret )
        {
            var client = new ClientRegister 
            { 
                CLientId ="mvctest",
                CLientSecret= "_mediatrdemo$",
                IdentityServer= "https://localhost:5001",
                Scope= "phaseCreditAPI"
            };
            return await Task.FromResult<ClientRegister>(client);
        }

        public async Task<GetAccessTokenResponse> GenerateJSONWebToken(string clientId, string clientSecret, string scope, string identityUrl)
        {
            var response = new GetAccessTokenResponse();  
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
            if (disco.IsError)
            {
                response.Error = disco.Error;
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return await Task.FromResult<GetAccessTokenResponse>(response);
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                response.Error = tokenResponse.Error;
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return await Task.FromResult<GetAccessTokenResponse>(response);
            }
            response.AccessToken = tokenResponse.AccessToken;
            response.ResponseCode = System.Net.HttpStatusCode.Created;
            return await Task.FromResult<GetAccessTokenResponse>(response);

           
        }

       

        public Task<IEnumerable<ClientRegister>> GetClientRegistersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
