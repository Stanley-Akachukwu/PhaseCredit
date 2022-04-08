using IdentityModel.Client;

namespace PhaseCredit.API.Test.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
