using EpicorBTWRest.Rules.BusinessObjects;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<LoginService> log;
        BOLogin _BO;

        public LoginService(HttpClient _httpClient, ILogger<LoginService> _log)
        {
            httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
            log = _log ?? throw new ArgumentNullException(nameof(_log));
            _BO = new BOLogin(log);
        }

        public async Task<PetitionResponse> Login(EpicorCredentials epicorCredentials)
            => await _BO.Login(epicorCredentials);
    }
}
