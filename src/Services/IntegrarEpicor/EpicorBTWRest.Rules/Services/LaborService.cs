using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Models.Epicor.BOLabor;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Services
{
    public class LaborService : ILaborService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<LaborService> log;
        BOLabor _BO;

        public LaborService(HttpClient _httpClient, ILogger<LaborService> _log)
        {
            httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
            _BO = new BOLabor(log);
        }

        public async Task<PetitionResponse> CreateTimeDirectInInEpicor(IntermediaContext _context, EpicorCredentials epicorCredentials)
        {
            try
            {
                var labor = await _context.Epicor_Labors.Where(x=> !x.Sincronizado && x.Closed).ToListAsync();

                if (labor.Count == 0) throw new Exception("Respuesta nula de la base de datos");

                _BO.epicorCredentials = epicorCredentials;

                bool success = false;
                foreach(var _labor in labor)
                {
                    success = await _BO.CreateTimeDirectInEpicor(_labor);
                    success = await _BO.Actualizar(_context, _labor);
                    await _context.SaveChangesAsync();
                }

                return new PetitionResponse()
                {
                    success = success
                };
            }
            catch (Exception ex)
            {
                log.LogError($"Se presentó un Error { ex }");
                return new PetitionResponse() 
                { 
                    success = false,
                    message =ex.Message,
                    result = ex.ToString()
                };
            }
        }

        public Task<PetitionResponse> GetNewLaborDtlNoHdr(Labor labor)
        {
            throw new NotImplementedException();
        }

    }
}
