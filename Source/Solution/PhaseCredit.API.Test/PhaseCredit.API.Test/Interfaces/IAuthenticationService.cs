using PhaseCredit.Core.DTOs.Authentications;

namespace PhaseCredit.API.Test.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> AuthenticateAsync(UserLoginRequest request);
    }
}
