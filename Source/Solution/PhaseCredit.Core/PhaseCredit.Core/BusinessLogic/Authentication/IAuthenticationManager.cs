using PhaseCredit.Core.DTOs.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.BusinessLogic.Authentication
{
    public interface IAuthenticationManager
    {
        Task<IdentityModelTokenResponse> GenerateJSONWebToken(Claim[] claims, string clientId, string clientSecret,string scope, string identityUrl);
    }
}
