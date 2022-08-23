using Microsoft.Extensions.Logging;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class DatosDBIntermedia
    {

        ILogger log { get; set; }

        public DatosDBIntermedia(ILogger _log)
        {
            log = _log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<PetitionResponse> GetDatosDBIntermediaAsync()
        {
            
            return new PetitionResponse
            {
                success = true,
                message = "Se realizó"
            };
        }
    }
}
