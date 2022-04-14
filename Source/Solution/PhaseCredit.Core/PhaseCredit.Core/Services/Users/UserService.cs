using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.Services.Users
{
    public class UserService: IUserService
    {
        public UserService()
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

        
        public async Task<UserLoginResponse> GenerateUserTokenAsync(Claim[] claims)
        {
            //----Client jwt configuration----
            var response = new UserLoginResponse();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phasecredit-for-soledealler01"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://www.phasecredit.com",
                audience: "https://www.phasecredit.com",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials,
                claims: claims
                );
            //  _logService.Log("Token generated", token, LogLevel.Information);
            response.UserToken = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult<UserLoginResponse>(response);
        }
    }
}
