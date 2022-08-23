using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Responses;
using System;

namespace EpicorBTWRest.Rules.Services
{
    public  class UD05Service : IUD05Service
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

        public async Task<PetitionResponse> GetNewUD05(string UD05, EpicorCredentials epicorCredentials)
            => await _BO.GetNewUD05(UD05, epicorCredentials);
    }
}
