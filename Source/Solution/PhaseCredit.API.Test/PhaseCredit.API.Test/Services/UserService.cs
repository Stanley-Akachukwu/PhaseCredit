using IdentityModel.Client;
using Newtonsoft.Json;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Users;
using System.Text.Json;

namespace PhaseCredit.API.Test.Services
{
    public class UserService : IUserService
    {
        private readonly IAppSettings _appSettings;
        public UserService(IAppSettings appSettings)
        {
            _appSettings=appSettings;   
        }
        public async Task<UsersResponse> GetListAsync(string accessToken)
        {
            var getUsersResponse = new UsersResponse();

            using (var httpClient = new HttpClient())
            {
                //httpClient.SetBearerToken(accessToken);
                using (var response = await httpClient.GetAsync(_appSettings.PhaseCreditAPIUrl+ _appSettings.PhaseCreditAPIGetUsers))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errors = new List<string>();
                        errors.Add(response.ReasonPhrase);
                        getUsersResponse = new UsersResponse()
                        {
                            ResponseCode = response.StatusCode,
                            ErrorMessages = errors
                        };
                    }
                    else
                    {
                        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                        var data = System.Text.Json.JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
                        getUsersResponse = JsonConvert.DeserializeObject<UsersResponse>(data);
                    }
                }
            }

            return getUsersResponse;
        }
    }
}
