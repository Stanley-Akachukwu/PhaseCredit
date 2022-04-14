using IdentityModel.Client;
using Newtonsoft.Json;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Authentications;

namespace PhaseCredit.API.Test.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request, string accessToken)
        {
            var userLoginResponse = new UserLoginResponse();
            using (var httpClient = new HttpClient())
            {
                httpClient.SetBearerToken(accessToken);
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:5445/api/users/login", request))
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
