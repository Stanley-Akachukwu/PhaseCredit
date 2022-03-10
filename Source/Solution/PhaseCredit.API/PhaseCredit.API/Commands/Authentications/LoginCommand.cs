

using PhaseCredit.Core.DTOs.Authentications;
using SimpleSoft.Mediator;

namespace PhaseCredit.API.Commands.Authentications
{
    public class LoginCommand : Command<UserLoginResponse>
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
