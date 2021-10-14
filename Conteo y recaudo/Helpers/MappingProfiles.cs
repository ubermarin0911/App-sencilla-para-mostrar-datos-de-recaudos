using AutoMapper;
using Conteo_y_recaudo.Dto;
using Conteo_y_recaudo.Entities;

namespace Conteo_y_recaudo.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recaudo, RecaudoARetornarDto>().
                ForMember(d => d.Fecha, o => o.MapFrom(s => s.Fecha.ToString("dd/MM/yyyy")));
            CreateMap<DataReporte, DataReporteDto>();
            CreateMap<DataRecaudosFechaEstacion, DataRecaudosFechaEstacionDto>();
            CreateMap<FechaCantidadValor, FechaCantidadValorDto>().
                ForMember(d => d.Fecha, o => o.MapFrom(s => s.Fecha.ToString("dd/MM/yyyy")));
            CreateMap<RecaudosPorEstacion, RecaudosPorEstacionDto>();
        }
    }
}