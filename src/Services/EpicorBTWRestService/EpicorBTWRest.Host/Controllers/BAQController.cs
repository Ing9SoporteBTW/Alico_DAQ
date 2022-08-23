using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.Rules.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharedService.Models;
using SharedService.Models.Epicor.BAQ;
using SharedService.Responses;

namespace EpicorBTWRest.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Epicor BAQ")]
    public class BAQController : ControllerBase
    {
        private readonly ILogger<BAQController> log;
        private readonly IConfiguration configuracion;
        private readonly IBAQService services;
        private readonly IntermediaContext _context;

        public BAQController(ILogger<BAQController> _log, IConfiguration _configuracion, IBAQService _services, IntermediaContext context) =>
            (log, configuracion, services, _context) = (_log ?? throw new ArgumentNullException(nameof(_log)), _configuracion ?? throw new ArgumentNullException(nameof(_configuracion)), _services ?? throw new ArgumentNullException(nameof(_services)), context);

        /// <summary>
        /// Consulta BAQ
        /// </summary>
        /// <param name="BAQConsult">Objeto para realizar la consulta BAQ.</param>
        /// <returns>BAQ Result</returns>
        /// <response code="200">Resultado de la consulta BAQ</response>
        /// <response code="400">Error en el proceso</response>  
        /// 
        [HttpPost, Route("BAQConsult")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> BAQConsult(BAQConsult BAQConsult)
            => await services.BAQConsult(BAQConsult);


        [HttpPost, Route("TestConsumo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<PetitionResponse> TestConsumo(BAQConsult BAQConsult)
            => await services.TestConsumo(_context,BAQConsult);

    }
}
