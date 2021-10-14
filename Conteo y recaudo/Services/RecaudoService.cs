using Conteo_y_recaudo.Data;
using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Interfaces;
using Conteo_y_recaudo.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Services
{
    public class RecaudoService : IRecaudoService
    {
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RecaudoContext _context;
        

        public RecaudoService(IUnitOfWork unitOfWork, IConfiguration config, RecaudoContext context)
        {
            _unitOfWork = unitOfWork;
            _config = config;
            _context = context;
        }

        public async Task<RecaudoData> GetRecaudosAsync(RecaudoSpecParams recaudoParams)
        {
            var spec = new RecaudoSpecification(recaudoParams);
            var countSpec = new RecaudoConFiltrosCantidadSpecification(recaudoParams);
            int totalItems = await _unitOfWork.Repository<Recaudo>().CountAsync(countSpec);
            var recaudos = await _unitOfWork.Repository<Recaudo>().ListAsync(spec);

            var recaudosData = new RecaudoData
            {
                Recaudos = recaudos,
                TotalItems = totalItems
            };

           

            return recaudosData;
        }

        private async Task<string> AutenticarAPIRecaudos(CredencialApiRecaudos credenciales)
        {
            string baseUrl = _config["ApiRecaudo:BaseUrl"];
            LoginResponse loginResponse = null;

            using (var httpClient = new HttpClient())
            {
                var credencialesJson = await Task.FromResult(JsonConvert.SerializeObject(credenciales));
                var httpContent = new StringContent(credencialesJson, Encoding.UTF8, "application/json");
                var httpResponse = await httpClient.PostAsync($"{baseUrl}Login", httpContent);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                    return loginResponse.token;
                }
            }
            return null;
        }

        private IEnumerable<DateTime> RangoDias(DateTime fechaInicial, DateTime fechaFinal, int intervalo)
        {
            for (var dia = fechaInicial.Date; dia.Date < fechaFinal.Date; dia = dia.AddDays(intervalo))
                yield return dia;
        }

        public async Task InsertarRecaudosAsync(ConsultaRecaudo consultaRecaudo)
        {
            string baseUrl = _config["ApiRecaudo:BaseUrl"];
            string token = await AutenticarAPIRecaudos(consultaRecaudo.credencial);
            List<Recaudo> listaRecaudos;

            DateTime dtFechaConsulta = DateTime.Parse(consultaRecaudo.fechaConsulta);

            foreach (DateTime dia in RangoDias(dtFechaConsulta, DateTime.Now, 1))
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var httpResponse = await httpClient.GetAsync($"{baseUrl}RecaudoVehiculos/{dia.ToString("yyyy-MM-dd")}");
                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();
                        listaRecaudos = JsonConvert.DeserializeObject<List<Recaudo>>(responseContent);
                        if (listaRecaudos != null)
                        {
                            listaRecaudos.ToList().ForEach(c => c.Fecha = dia);
                            foreach (var recaudo in listaRecaudos)
                                _unitOfWork.Repository<Recaudo>().Add(recaudo);

                            await _unitOfWork.Complete();
                        }
                    }
                }
            }
        }

        public async Task<DataReporte> GetDataReporteRecaudo(RecaudoSpecParams recaudoParams)
        {
            DateTime fechaUltimos3Meses = DateTime.Now.AddMonths(-3);

            DataReporte recaudos;
            List<ConsultaRecaudosPorFechaYEstacion> lstConsultaRecaudosPorFechaYEstacion;
            List<RecaudosPorEstacion> recaudosPorEstacion;
            List<DataRecaudosFechaEstacion> lstDataRecaudosPorFechaYEstacion = new List<DataRecaudosFechaEstacion>();
            DataRecaudosFechaEstacion dataRecaudosFechaEstacion;

            lstConsultaRecaudosPorFechaYEstacion = await _context.Recaudos.
                 Where(r => r.Fecha >= fechaUltimos3Meses).
                 GroupBy(r => new { r.Estacion, r.Fecha }).
                 OrderBy(g => g.Key.Fecha).
                 Select(g => new ConsultaRecaudosPorFechaYEstacion
                 {
                     Estacion = g.Key.Estacion,
                     Fecha = g.Key.Fecha,
                     TotalCantidad = g.Count(),
                     TotalValor = g.Sum(c => c.ValorTabulado).ToString()
                 })
                 .Skip(recaudoParams.PageSize * (recaudoParams.PageIndex - 1))
                 .Take(recaudoParams.PageSize)
                 .ToListAsync();

            lstDataRecaudosPorFechaYEstacion.Clear();
            foreach (var consultaRecaudo in lstConsultaRecaudosPorFechaYEstacion)
            {
                FechaCantidadValor fechaCantidadValor = new FechaCantidadValor
                {
                    Fecha = consultaRecaudo.Fecha,
                    TotalCantidad = consultaRecaudo.TotalCantidad,
                    TotalValor = consultaRecaudo.TotalValor
                };

                dataRecaudosFechaEstacion = lstDataRecaudosPorFechaYEstacion.
                  Select(r => r).Where(r => r.Estacion == consultaRecaudo.Estacion).FirstOrDefault();

                if (dataRecaudosFechaEstacion != null)
                {
                    dataRecaudosFechaEstacion.FechaCantidadValor.Add(fechaCantidadValor);
                }
                else
                {
                    dataRecaudosFechaEstacion = new DataRecaudosFechaEstacion();
                    dataRecaudosFechaEstacion.FechaCantidadValor = new List<FechaCantidadValor>();

                    dataRecaudosFechaEstacion.Estacion = consultaRecaudo.Estacion;
                    dataRecaudosFechaEstacion.FechaCantidadValor.Add(fechaCantidadValor);
                    lstDataRecaudosPorFechaYEstacion.Add(dataRecaudosFechaEstacion);
                }
            }

            recaudosPorEstacion = await _context.Recaudos.
                 Where(r => r.Fecha >= fechaUltimos3Meses).
                 GroupBy(r => new { r.Estacion}).
                 Select(g => new RecaudosPorEstacion
                 {
                     Estacion = g.Key.Estacion,
                     TotalCantidad = g.Count(),
                     TotalValor = g.Sum(c => c.ValorTabulado).ToString()
                 }).ToListAsync();

            foreach (var consultaRecaudo in recaudosPorEstacion)
            {
                RecaudosPorEstacion recaudosEstacion = new RecaudosPorEstacion
                {
                    Estacion = consultaRecaudo.Estacion,
                    TotalCantidad = consultaRecaudo.TotalCantidad,
                    TotalValor = consultaRecaudo.TotalValor
                };

                dataRecaudosFechaEstacion = lstDataRecaudosPorFechaYEstacion.
                Select(r => r).Where(r => r.Estacion == consultaRecaudo.Estacion).FirstOrDefault();

                if (dataRecaudosFechaEstacion != null)
                {
                    dataRecaudosFechaEstacion.RecaudosEstacion = recaudosEstacion;
                }
            }

            var recaudosTotalCantidad = await _context.Recaudos.Where(r => r.Fecha >= fechaUltimos3Meses).CountAsync();
            var recaudosTotalValor = await _context.Recaudos.Where(r => r.Fecha >= fechaUltimos3Meses).SumAsync(s => s.ValorTabulado);

            recaudos = new DataReporte
            {
                DataRecaudosFechaEstacion = lstDataRecaudosPorFechaYEstacion,
                TotalCantidad = recaudosTotalCantidad,
                TotalValor = recaudosTotalValor
            };

            return  recaudos;
        }
    }
}