using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Core.Services.Logs;
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
        private readonly ILogService _logService;
        public AuthenticationManager(ILogService logService)
        {
            _logService = logService;
        }
        public async Task<IdentityModelTokenResponse> GenerateJSONWebToken(Claim[] claims, string clientId, string clientSecret, string scope, string identityUrl)
        {
            var response = new IdentityModelTokenResponse();  
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync(identityUrl);
            if (disco.IsError)
            {
                response.Error = disco.Error;
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return await Task.FromResult<IdentityModelTokenResponse>(response);
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            if (tokenResponse.IsError)
            {
                response.Error = tokenResponse.Error;
                response.ResponseCode = System.Net.HttpStatusCode.BadRequest;
                return await Task.FromResult<IdentityModelTokenResponse>(response);
            }
            response.AccessToken = tokenResponse.AccessToken;
            response.ResponseCode = System.Net.HttpStatusCode.Created;
            return await Task.FromResult<IdentityModelTokenResponse>(response);

            ////----Client jwt configuration----
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("phasecredit-for-soledealler01"));
            //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    issuer: "https://www.phasecredit.com",
            //    audience: "https://www.phasecredit.com",
            //    expires: DateTime.Now.AddHours(3),
            //    signingCredentials: credentials,
            //    claims: claims
            //    );
            //_logService.Log("Token generated", token, LogLevel.Information);
            //var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            //return Task.FromResult<String>(accessToken);
        }
    }
}
