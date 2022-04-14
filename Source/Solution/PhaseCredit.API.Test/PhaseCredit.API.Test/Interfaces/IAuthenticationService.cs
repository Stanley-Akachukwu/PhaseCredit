using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.DTOs.ClientAuthorization;

namespace PhaseCredit.API.Test.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> LoginAsync(UserLoginRequest request, string accessToken);
    }
}
