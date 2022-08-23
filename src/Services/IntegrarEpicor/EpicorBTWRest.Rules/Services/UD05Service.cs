using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Models.Epicor.BOUD05;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Services
{
    public class UD05Service : IUD05Service
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<UD05Service> log;
        BOUD05 _BO;

        public UD05Service(HttpClient _httpClient, ILogger<UD05Service> _log)
        {
            httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
            _BO = new BOUD05(log);
        }

        public async Task<PetitionResponse> CreateLostTimeInEpicor(IntermediaContext _context, EpicorCredentials epicorCredentials)
        {
            try
            {
                var ud = await _context.Epicor_UD05s.Where(x => !x.Sincronizado && x.Closed ).ToListAsync();

                if (ud.Count == 0) throw new Exception("No se tiene datos para procesar");

                _BO.epicorCredentials = epicorCredentials;

                bool success = false;
                foreach (var _ud in ud)
                {
                    success = await _BO.CreateLostTimeInEpicor(_ud);
                    success = await _BO.Actualizar(_context,_ud);
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
                    message = ex.Message,
                    result = ex.ToString()
                };
            }
        }

        public Task<PetitionResponse> GetaNewUD05(UD05 ud05)
        {
            throw new NotImplementedException();
        }
    }
}
