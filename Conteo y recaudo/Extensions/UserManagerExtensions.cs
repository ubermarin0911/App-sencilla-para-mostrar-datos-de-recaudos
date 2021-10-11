using Conteo_y_recaudo.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<Usuario> FindByEmailFromClaimsPrinciple(this UserManager<Usuario> input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
