using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using SmartEnergy.Contract.DTO;
using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Service.Services
{
    public class AuthHelperService : IAuthHelperService
    {
        private readonly IConfigurationSection _googleSettings;

        public AuthHelperService(IConfiguration config)
        {
            _googleSettings = config.GetSection("GoogleAuthSettings");
        }

        public int GetUserIDFromPrincipal(ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginDto externalLogin)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _googleSettings.GetSection("clientId").Value }
                };
                var payload = await GoogleJsonWebSignature.ValidateAsync(externalLogin.IdToken, settings);
                return payload;
            }
            catch (Exception ex)
            {
                //log an exception
                return null;
            }
        }
    }
}
