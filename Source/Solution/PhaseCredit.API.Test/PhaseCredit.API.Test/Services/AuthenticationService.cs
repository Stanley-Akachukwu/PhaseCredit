using Newtonsoft.Json;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Authentications;
using System.Security.Claims;

namespace PhaseCredit.API.Test.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<UserLoginResponse> AuthenticateAsync(UserLoginRequest request)
        {
            var userLoginResponse = new UserLoginResponse();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:7174/api/authentication/login", request))
                {
                    if (response != null)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        userLoginResponse = JsonConvert.DeserializeObject<UserLoginResponse>(apiResponse);
                    }
                }
            }
            return userLoginResponse;
        }
    }
}
