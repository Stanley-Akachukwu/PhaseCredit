using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs.Authentications
{
    public class IdentityModelTokenResponse
    {
        public string AccessToken { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public string Error { get; set; }
    }
}
