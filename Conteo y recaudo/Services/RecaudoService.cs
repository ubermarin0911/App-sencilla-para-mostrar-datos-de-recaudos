using Conteo_y_recaudo.Entities;
using Conteo_y_recaudo.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Conteo_y_recaudo.Services
{
    public class RecaudoService : IRecaudoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RecaudoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Recaudo>> GetRecaudosAsync()
        {
            return await _unitOfWork.Repository<Recaudo>().ListAllAsync();
        }

        public async Task<Recaudo> InsertarRecaudosAsync(List<Recaudo> recaudo)
        {
            throw new NotImplementedException();
        }
    }
}