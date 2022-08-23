using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.Models;
using SharedService.Models.Epicor.BAQ;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOBAQ
    {
        private const string strModule = "BAQ";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;
        public BOBAQ(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<PetitionResponse> BAQConsult(BAQConsult baqConsult)
        {
            string strUrlMethod = strUrl + "/BAQConsult";
            try
            {
                string method = $"BaqSvc/{baqConsult.BAQName}({baqConsult.CompanyID})/";

                string inst = string.Empty;

                if (!string.IsNullOrEmpty(baqConsult.parameters))
                    inst = "?" + baqConsult.parameters;
                if (!string.IsNullOrEmpty(baqConsult.select))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$select=" + baqConsult.select;
                if (!string.IsNullOrEmpty(baqConsult.filter))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$filter=" + baqConsult.filter;
                if (!string.IsNullOrEmpty(baqConsult.orderby))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$orderby=" + baqConsult.orderby;
                if (!string.IsNullOrEmpty(baqConsult.top))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$top=" + baqConsult.top;
                if (!string.IsNullOrEmpty(baqConsult.skip))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$skip=" + baqConsult.skip;
                if (!string.IsNullOrEmpty(baqConsult.inlinecount))
                    inst += (string.IsNullOrEmpty(inst) ? "?" : "&") + "$inlinecount=" + baqConsult.inlinecount;

                string type = "value";
                EpicorCredentials epicorCredentials = baqConsult.EpicorCredentials;

                var response = await epicorRequests.Get(method, inst, type, epicorCredentials);
                if (response != null)
                {
                    if (!(response is Newtonsoft.Json.Linq.JArray))
                        throw new Exception(response.ToString());

                    List<object> result = JsonConvert.DeserializeObject<List<object>>(response.ToString());
                    if (result.Count <= 0)
                        return Shared.Answer(false, "La consulta no tiene registros para los parametros y/o filtros especificados", strModule, strUrlMethod, null);

                    return Shared.Answer(true, "Consulta BAQ realizada con éxito!!!", strModule, strUrlMethod, JsonConvert.SerializeObject(response));
                }
                else
                    return Shared.Answer(false, "No fue posible realizar la consulta BAQ", strModule, strUrlMethod, null);
            }
            catch (Exception ex)
            {
                log.LogError("***ERROR CONSULTA EPICOR BAQ {0}", JsonConvert.SerializeObject(ex, Formatting.Indented));
                return Shared.Answer(false, $"Error en Consulta BAQ: {ex.Message}", strModule, strUrlMethod, null);
            }
        }

       
    }
}
