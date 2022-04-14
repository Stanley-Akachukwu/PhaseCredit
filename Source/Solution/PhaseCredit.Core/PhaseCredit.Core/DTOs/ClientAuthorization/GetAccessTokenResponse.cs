using System.Net;

namespace PhaseCredit.Core.DTOs.ClientAuthorization
{
    public class GetAccessTokenResponse
    {
        public string AccessToken { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public string Error { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
