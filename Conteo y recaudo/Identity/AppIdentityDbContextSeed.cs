using Conteo_y_recaudo.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new Usuario
                {
                    Nombre = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Direccion = "Calle falsa 123"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
