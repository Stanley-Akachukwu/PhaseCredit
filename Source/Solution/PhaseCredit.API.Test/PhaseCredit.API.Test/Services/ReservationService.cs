using IdentityModel.Client;
using Newtonsoft.Json;
using PhaseCredit.API.Test.Interfaces;
using PhaseCredit.Core.DTOs.Resrvations;
using System.Net.Http.Headers;
using System.Text.Json;

namespace PhaseCredit.API.Test.Services
{
    public class ReservationService : IReservationService
    {
        public async Task<ListReservationResponse> GetListAsync(string jwt)
        {
            var listReservationResponse = new ListReservationResponse();

            using (var httpClient = new HttpClient())
            {
                httpClient.SetBearerToken(jwt);
                using (var response = await httpClient.GetAsync("https://localhost:5445/api/Reservation/reservations"))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errors = new List<string>();
                        errors.Add(response.ReasonPhrase);
                        listReservationResponse=  new ListReservationResponse()
                        {
                            ResponseCode=response.StatusCode,
                            ErrorMessages= errors
                        };
                    }
                    else
                    {
                        var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
                        var data = System.Text.Json.JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
                        listReservationResponse = JsonConvert.DeserializeObject<ListReservationResponse>(data);
                    }
                }
            }
            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            //    using (var response = await httpClient.GetAsync("https://localhost:5445/api/Reservation/reservations"))  
            //    {
            //        if (response!=null)
            //        {
            //            string apiResponse = await response.Content.ReadAsStringAsync();
            //            listReservationResponse = JsonConvert.DeserializeObject<ListReservationResponse>(apiResponse);
            //        }
            //    }
            //}
            return listReservationResponse;
        }
    }
}

 