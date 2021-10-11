using Conteo_y_recaudo.Entities.Identity;

namespace Conteo_y_recaudo.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
