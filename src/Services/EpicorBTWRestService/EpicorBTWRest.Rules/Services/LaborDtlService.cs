using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicorBTWRest.Rules.Services
{
    public class LaborDtlService : ILaborDtlService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<LaborDtlService> log;
        BOLaborDtl _BO;

        public LaborDtlService(HttpClient _httpClient, ILogger<LaborDtlService> _log)
        {
            httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
            _BO = new BOLaborDtl(log);
        }

        public async Task<PetitionResponse> GetNewLaborDtl(EpicorCredentials epicorCredentials)
            => await _BO.GetNewLaborDtl(epicorCredentials);

        
    }
}
