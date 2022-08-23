using EpicorBTWRest.DataAccess.DataContext;
using EpicorBTWRest.DataAccess.Models.Enum;
using EpicorBTWRest.DataAccess.Models.TimeReport;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.Models.Epicor.BAQ;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class ObtenerInformacion
    {

        delegate Task<bool> Proceso_Maquina(IntermediaContext context,List<TimeReport> t, ILogger log);
        ILogger log { get; set; }

        public ObtenerInformacion(ILogger _log)
        {
            log = _log ?? throw new ArgumentNullException(nameof(log)); 
        }
        
        public async Task<PetitionResponse> GetObtenerInformacionAsync(IntermediaContext context,BAQConsult consult)
        {
            //
            log.LogInformation("Solicitud información BAQ***");
            BOBAQ oBAQ = new BOBAQ(log);
            //
            PetitionResponse resultado = await oBAQ.BAQConsult(consult); // llamado del metodo

            if (resultado == null)
                throw new Exception("Se prensento un error");
            
            if (resultado != null && resultado.success)
            {
                var Tiempos = JsonConvert.DeserializeObject<List<TimeReport>>(resultado.result.ToString());

                if (Tiempos == null)
                    throw new Exception("La consulta no tiene registros");


                var primeraListaRecorrer = Tiempos.GroupBy(x => x.TimeReport_Trabajo);

                foreach (var list in primeraListaRecorrer)
                {

                    await Delegates.DelagadoProcesosMaquina.InicializarTrabajos(context, Tiempos.Where(x => x.TimeReport_Trabajo == list.Key).ToList(), log);
                    //var manejadoresEstado = new Dictionary<EstadosMaquina, Proceso_Maquina>
                    //{
                    //    { EstadosMaquina.Iniciado, Delegates.DelagadoProcesosMaquina.InicializarTrabajos}
                    //};
                    log.LogWarning($"Grupo a Trabajar {list.Key}");
                    log.LogWarning($"Registros a procesar {JsonConvert.SerializeObject(list, Formatting.Indented)}");
                    //Proceso_Maquina delegadoEstado = manejadoresEstado[0];
                    //await delegadoEstado(context,Tiempos.Where(x => x.TimeReport_Trabajo == list.Key).ToList(), log);
                }

            }
            else
                return new PetitionResponse 
                {
                    success = false,
                    message = "Algo paso"
                };

            return new PetitionResponse 
            {
                success =true,
                message = "Se realizó"
            };
        }
    }
}
