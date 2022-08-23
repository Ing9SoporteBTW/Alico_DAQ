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
    [ApiExplorerSettings(GroupName = "Epicor LaborDtl")]
    public class LaborDtlController : ControllerBase
    {
        private readonly ILogger<LaborDtlController> log;
        private readonly IConfiguration configuracion;
        private readonly ILaborDtlService services;

        public LaborDtlController(ILogger<LaborDtlController> _log, IConfiguration _configuracion, ILaborDtlService _services) =>
            (log, configuracion, services) = (_log ?? throw new ArgumentNullException(nameof(_log)), _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)), _services ?? throw new ArgumentNullException(nameof(_services)));

        /// <summary>
        /// Insertar un nuevo LaborDtl
        /// </summary>
        /// <param name="EpicorCredentials">Objeto para conectarse a Epicor.</param>
        /// <returns>COOneTime</returns>
        /// <response code="200">Resultado de LaborDtl</response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        [HttpPost, Route("GetNewLaborDtl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> GetNewLaborDtl(EpicorCredentials EpicorCredentials)
            => await services.GetNewLaborDtl(EpicorCredentials);
    }
}
