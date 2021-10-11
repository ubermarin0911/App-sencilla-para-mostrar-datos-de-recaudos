using AutoMapper;
using Conteo_y_recaudo.Dto;
using Conteo_y_recaudo.Entities;

namespace Conteo_y_recaudo.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Recaudo, RecaudoARetornarDto>();
        }
    }
}