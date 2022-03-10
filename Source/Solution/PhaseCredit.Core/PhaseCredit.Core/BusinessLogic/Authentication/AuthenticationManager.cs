using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.BusinessLogic.Authentication
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly ILogManager _logManager;
        public AuthenticationManager(ILogManager logManager)
        {
            _logManager = logManager;
        }
        public Task<string> GenerateJSONWebToken(Claim[] claims)
        {
            //----Client jwt configuration----
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phasecredit-for-soledealler01"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "https://www.phasecredit.com",
                audience: "https://www.phasecredit.com",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials,
                claims: claims
                );
            _logManager.Log("Token generated", token, LogLevel.Information);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult<String>(accessToken);
        }
    }
}
