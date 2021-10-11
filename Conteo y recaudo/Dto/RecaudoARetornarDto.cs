using System;

namespace Conteo_y_recaudo.Dto
{
    public class RecaudoARetornarDto
    {
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public short Hora { get; set; }
        public string Categoria { get; set; }
        public long ValorTabulado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
