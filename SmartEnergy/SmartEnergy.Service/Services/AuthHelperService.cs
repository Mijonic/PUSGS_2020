using SmartEnergy.Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SmartEnergy.Service.Services
{
    public class AuthHelperService : IAuthHelperService
    {
        public int GetUserIDFromPrincipal(ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
