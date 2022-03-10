using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs.Authentications
{
    public class UserLoginResponse : Response
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
    }
}
