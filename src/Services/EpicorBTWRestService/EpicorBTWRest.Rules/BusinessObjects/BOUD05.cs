using EpicorBTWRest.DataAccess.UD05;
using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedService.Models;
using SharedService.Models.Epicor.BOUD05;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOUD05
    {
        private const string strModule = "UD05";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;
        public BOUD05(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<PetitionResponse> GetNewUD05(string UD05, EpicorCredentials EpicorCredentials)
        {
            string strUrlMethod = strUrl + "/GetNewUD05UD05";
            try
            {
                GetNewUD05_input getUD05_input = new GetNewUD05_input()
                {
                    UD05 = UD05,
                };

                string method = "Ice.BO.UD05Svc/";
                string inst = "GetNewUD05";//GetaNewUD05
                string type = "value";
                string entity = JsonConvert.SerializeObject(getUD05_input);
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
