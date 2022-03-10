using Newtonsoft.Json;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Resrvations;
using System.Net.Http.Headers;

namespace PhaseCredit.API.Test.Services
{
    public class ReservationService : IReservationService
    {
        public async Task<ListReservationResponse> GetListAsync(string jwt)
        {
            var listReservationResponse = new ListReservationResponse();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                using (var response = await httpClient.GetAsync("https://localhost:7174/api/Reservation"))  
                {
                    if (response!=null)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        listReservationResponse = JsonConvert.DeserializeObject<ListReservationResponse>(apiResponse);
                    }
                }
            }
            return listReservationResponse;
        }
    }
}
