using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Models.Epicor.BAQ;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Services
{
    public class BAQService : IBAQService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<BAQService> log;
        BOBAQ _BO;
        ObtenerInformacion _class;

        public BAQService(HttpClient _httpClient, ILogger<BAQService> _log, IntermediaContext contexxt)
        {
            httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
            _BO = new BOBAQ(log);
            _class = new ObtenerInformacion(log);
        }

        public async Task<PetitionResponse> BAQConsult(BAQConsult baqConsult)
            => await _BO.BAQConsult(baqConsult);

        public async Task<PetitionResponse> TestConsumo(IntermediaContext context,BAQConsult baqConsult)
            => await _class.GetObtenerInformacionAsync(context,baqConsult);

    }
}
