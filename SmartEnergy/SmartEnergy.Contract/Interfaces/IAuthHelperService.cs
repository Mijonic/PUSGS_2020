using Google.Apis.Auth;
using SmartEnergy.Contract.DTO;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartEnergy.Contract.Interfaces
{
    public interface IAuthHelperService
    {
        int GetUserIDFromPrincipal(ClaimsPrincipal user);
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(ExternalLoginDto externalLogin);
    }
}
