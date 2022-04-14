using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PhaseCredit.Core.DTOs.ClientAuthorization;
using System.Web.Http.Controllers;
using System.Web.Http.Results;

namespace PhaseCredit.API.Test.Filters
{
    public class AuthTokenFilterAttribute: Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = new GetAccessTokenRequest { ClientId = "mvctest", ClientSecret = "_mediatrdemo$" };
            var accessTokenResponse = new GetAccessTokenResponse();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync("https://localhost:5445/api/clientAuth/accesstoken", request))
                {
                    if (response != null)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        accessTokenResponse = JsonConvert.DeserializeObject<GetAccessTokenResponse>(apiResponse);
                        if (!string.IsNullOrEmpty(accessTokenResponse.AccessToken))
                        {
                            context.HttpContext.Request.Headers.Add("AccessToken", accessTokenResponse.AccessToken);
                            await next();
                        }
                    }
                }
            }
        }
    }
}
