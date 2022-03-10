using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs
{
    public class Response
    {
        public string? ResponseMessage { get; set; }
        public int ResponseCode { get; set; }
        public List<string>? ErrorMessages { get; set; }
    }
}
