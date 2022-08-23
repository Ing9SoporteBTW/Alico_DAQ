using EpicorBTWRest.DataAccess.Models.LaborDtl;
using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOLaborDtl
    {
        private const string strModule = "LaborDtl";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;
        public BOLaborDtl(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<PetitionResponse> GetNewLaborDtl(EpicorCredentials EpicorCredentials)
        {
            string strUrlMethod = strUrl + "/GetNewLaborDtl";
            try
            {
                GetNewLaborDtlLaborDtl_input getLaborDtl_input = new GetNewLaborDtlLaborDtl_input()
                {
                    //LaborDtl = LaborDtl,
                };

                string method = "Erp.BO.LaborDtlSvc/";
                string inst = "GetNewLaborDtl";
                string type = "value";
                string entity = JsonConvert.SerializeObject(getLaborDtl_input);
                EpicorCredentials epicorCredentials = EpicorCredentials;

                var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);
                if (response != null)
                    return Shared.Answer(true, "Registro realizado con éxito!!!", strModule, strUrlMethod, JsonConvert.SerializeObject(response));
                else
                    return Shared.Answer(false, "No fue posible realizar el registro", strModule, strUrlMethod, null);
            }
            catch (Exception ex)
            {
                return Shared.Answer(false, ex.Message, strModule, strUrlMethod, null);
            }
        }
    }
}
