using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Data.Entities.ClientRegisters
{
    public class ClientRegister
    {
        public string CLientId { get; set; }
        public string CLientSecret { get; set; }
        public string Scope { get; set; }
        public string IdentityServer { get; set; }
    }
}
