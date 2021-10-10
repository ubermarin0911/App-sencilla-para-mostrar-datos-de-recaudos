using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Controllers
{
    public class RecaudosController : BaseApiController
    {
        private readonly IRecaudoService _recaudoService;

        public RecaudosController(IRecaudoService recaudoService)
        {
            _recaudoService = recaudoService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Recaudo>>> GetRecaudos()
        {
            var recaudos = await _recaudoService.GetRecaudosAsync();

            return Ok(recaudos);
        }

        [HttpPost]
        public async Task InsertarRecaudosAsync(ConsultaRecaudo consultaRecaudo)
        {
            await _recaudoService.InsertarRecaudosAsync(consultaRecaudo);
        }
    }
}