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
    [ApiExplorerSettings(GroupName = "Epicor Labor")]
    public class LaborController : ControllerBase
    {
        private readonly ILogger<LaborController> log;
        private readonly IConfiguration configuracion;
        private readonly ILaborService services;
        private readonly IntermediaContext context;

        public LaborController(ILogger<LaborController> _log, IConfiguration _configuracion, ILaborService _services, IntermediaContext _context) =>
            (log, configuracion, services, context) = 
            (
                _log ?? throw new ArgumentNullException(nameof(_log)),
                    _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)), 
                        _services ?? throw new ArgumentNullException(nameof(_services)),
                            _context ?? throw new ArgumentNullException(nameof(_context))
            );

        /// <summary>
        /// Crear nuevo registro de mano de obra
        /// </summary>
        /// <param name="_labor">Objeto para conectarse a Epicor.</param>
        /// <returns>COOneTime</returns>
        /// <response code="200">Resultado del COOneTime consultado</response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        /// 
        /// 
        [HttpPost, Route("CreateTimeDirectInInEpicor")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> CreateTimeDirectInInEpicor(EpicorCredentials epicorCredentials)
            => await services.CreateTimeDirectInInEpicor(context, epicorCredentials);




    }
}
