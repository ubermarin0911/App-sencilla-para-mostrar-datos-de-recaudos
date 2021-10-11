namespace Conteo_y_recaudo.Specifications
{
    public class RecaudoSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Estacion { get; set; }
        public string Sentido { get; set; }
        public string Categoria { get; set; }
        public string Ordenar { get; set; }
        private string _buscar;
        public string Buscar
        {
            get => _buscar;
            set => _buscar = value.ToLower();
        }
    }
}
