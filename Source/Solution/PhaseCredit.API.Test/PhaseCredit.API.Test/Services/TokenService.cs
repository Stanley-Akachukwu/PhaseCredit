using IdentityModel.Client;
using Microsoft.Extensions.Options;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.API.Test.Models;

namespace PhaseCredit.API.Test.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> IdentityServerSettings;
        public Task<TokenResponse> GetToken(string scope)
        {
            throw new NotImplementedException();
        }
    }
}
