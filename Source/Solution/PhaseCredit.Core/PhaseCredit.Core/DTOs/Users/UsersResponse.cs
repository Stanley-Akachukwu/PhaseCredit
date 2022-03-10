using PhaseCredit.Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.DTOs.Users
{
    public class UsersResponse: Response
    {
        public List<User> Users { get; set; }
    }
}
