using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Interfaces;
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
        public RecaudoService(IUnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<IReadOnlyList<Recaudo>> GetRecaudosAsync()
        {
            return await _unitOfWork.Repository<Recaudo>().ListAllAsync();
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
    }
}