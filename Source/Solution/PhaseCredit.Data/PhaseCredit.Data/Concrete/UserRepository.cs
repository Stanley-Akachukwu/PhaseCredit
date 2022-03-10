using Microsoft.IdentityModel.Tokens;
using PhaseCredit.Data.Abstract;
using PhaseCredit.Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Data.Concrete
{
    public class UserRepository : IUserRepository
    {
        // private readonly ApiDbContext _context;
        public UserRepository()
        {
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await GetDummyUsers();
            return await Task.FromResult(users);
        }

        public async Task<List<User>> GetDummyUsers()
        {
            var userList = new List<User> {
            new User { UserName = "jack", Password = "jack", Role = "Admin" },
            new User { UserName = "donald", Password = "donald", Role = "Manager" },
            new User { UserName = "thomas", Password = "thomas", Role = "Developer" }
         };
            return await Task.FromResult(userList);
        }

        public async Task<User> FindUserAsync(string username)
        {
            var user = GetDummyUsers().Result.Where(a => a.UserName == username).FirstOrDefault();
            return await Task.FromResult(user);   
        }
    }
}
