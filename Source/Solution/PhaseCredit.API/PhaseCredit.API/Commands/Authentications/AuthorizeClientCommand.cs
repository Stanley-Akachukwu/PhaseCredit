using PhaseCredit.API.Handlers;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.DTOs.ClientAuthorization;

namespace PhaseCredit.API.Commands.Authentications
{
    public class AuthorizeClientCommand : Command<GetAccessTokenResponse>
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
