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
    [ApiExplorerSettings(GroupName = "Epicor UD05")]
    public  class UD05Controller : ControllerBase
    {
        private readonly ILogger<UD05Controller> log;
        private readonly IConfiguration configuracion;
        private readonly IUD05Service services;

        public UD05Controller(ILogger<UD05Controller> _log, IConfiguration _configuracion, IUD05Service _services) =>
            (log, configuracion, services) = (_log ?? throw new ArgumentNullException(nameof(_log)), _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)), _services ?? throw new ArgumentNullException(nameof(_services)));

        /// <summary>
        /// Insertar un nuevo registro en la UD05
        /// </summary>
        /// <param name="UD05">UD05</param>
        /// <param name="EpicorCredentials">Objeto para conectarse a Epicor.</param>
        /// <returns>COOneTime</returns>
        /// <response code="200">Resultado de la insercción en UD05 </response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        [HttpPost, Route("GetNewUD05")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> GetNewUD05(string UD05, EpicorCredentials EpicorCredentials)
            => await services.GetNewUD05(UD05, EpicorCredentials);
    }
}
