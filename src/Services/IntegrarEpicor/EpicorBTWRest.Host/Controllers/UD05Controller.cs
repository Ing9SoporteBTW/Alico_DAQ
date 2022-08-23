using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Ice UD05")]
    public class UD05Controller : ControllerBase
    {
        private readonly ILogger<UD05Controller> log;
        private readonly IConfiguration configuracion;
        private readonly IUD05Service services;
        private readonly IntermediaContext context;

        public UD05Controller(ILogger<UD05Controller> _log, IConfiguration _configuracion, IUD05Service _services, IntermediaContext _context) =>
            (log, configuracion, services, context) =
            (
                _log ?? throw new ArgumentNullException(nameof(_log)),
                    _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)),
                        _services ?? throw new ArgumentNullException(nameof(_services)),
                            _context ?? throw new ArgumentNullException(nameof(_context))
            );


        /// <summary>
        /// Crear nuevo registro de tiempo perdido
        /// </summary>
        /// <param name="_ud">Objeto para conectarse a Epicor.</param>
        /// <returns>COOneTime</returns>
        /// <response code="200">Resultado de la solicitud</response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        /// 
        /// 
        [HttpPost, Route("CreateLostTimeInEpicor")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> CreateLostTimeInEpicor(EpicorCredentials epicorCredentials)
            => await services.CreateLostTimeInEpicor(context, epicorCredentials);

    }
}
