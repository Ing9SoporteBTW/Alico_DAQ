using EpicorBTWRest.Rules.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Epicor Login")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> log;
        private readonly IConfiguration configuracion;
        private readonly ILoginService services;

        public LoginController(ILogger<LoginController> _log, IConfiguration _configuracion, ILoginService _services) =>
            (log, configuracion, services) = (_log ?? throw new ArgumentNullException(nameof(_log)), _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)), _services ?? throw new ArgumentNullException(nameof(_services)));

        /// <summary>
        /// Consulta la compañia por su ID
        /// </summary>
        /// <param name="EpicorCredentials">Objeto para conectarse a Epicor.</param>
        /// <returns>Copmañia</returns>
        /// <response code="200">Resultado de compañia consultada</response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        [HttpPost, Route("Login")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> Login(EpicorCredentials EpicorCredentials)
            => await services.Login(EpicorCredentials);

    }
}
