using Microsoft.AspNetCore.Identity;

namespace Conteo_y_recaudo.Entities.Identity
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    }
}
