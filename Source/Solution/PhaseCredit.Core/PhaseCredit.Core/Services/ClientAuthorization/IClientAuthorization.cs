using PhaseCredit.Core.DTOs.ClientAuthorization;
using PhaseCredit.Data.Entities.ClientRegisters;

namespace PhaseCredit.Core.Services.ClientAuthorization
{
    public interface IClientAuthorization
    {
        Task<GetAccessTokenResponse> GenerateJSONWebToken(string clientId, string clientSecret,string scope, string identityUrl);
        Task<IEnumerable<ClientRegister>> GetClientRegistersAsync();
        Task<ClientRegister> FindClientRegisterAsync(string clientId, string clientSecret);
    }
}
