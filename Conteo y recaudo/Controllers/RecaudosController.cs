using AutoMapper;
using Conteo_y_recaudo.Dto;
using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Helpers;
using Conteo_y_recaudo.Interfaces;
using Conteo_y_recaudo.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Controllers
{
   
    public class RecaudosController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IRecaudoService _recaudoService;

        public RecaudosController(IRecaudoService recaudoService, IMapper mapper)
        {
            _recaudoService = recaudoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<RecaudoARetornarDto>>> GetRecaudos(
            [FromQuery] RecaudoSpecParams recaudoParams)
        {
            var recaudosData = await _recaudoService.GetRecaudosAsync(recaudoParams);

            var data = _mapper
           .Map<IReadOnlyList<Recaudo>, IReadOnlyList<RecaudoARetornarDto>>(recaudosData.Recaudos);

            return Ok(new Pagination<RecaudoARetornarDto>(recaudoParams.PageIndex,
            recaudoParams.PageSize, recaudosData.TotalItems, data));
        }

        [HttpPost]
        public async Task InsertarRecaudosAsync(ConsultaRecaudo consultaRecaudo)
        {
            await _recaudoService.InsertarRecaudosAsync(consultaRecaudo);
        }

        [HttpGet("dataReporteRecaudo")]
        public async Task<ActionResult<Pagination<DataReporteDto>> > GetDataReporteRecaudo([FromQuery] RecaudoSpecParams recaudoParams)
        {
            var dataReporteRecaudo = await _recaudoService.GetDataReporteRecaudo(recaudoParams);

            var data = _mapper.Map<DataReporte, DataReporteDto>(dataReporteRecaudo);

            return Ok(new Pagination<DataReporteDto>(recaudoParams.PageIndex,
            recaudoParams.PageSize, dataReporteRecaudo.TotalItems, data));
        }
    }
}