using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOLogin
    {
        private const string strModule = "Login";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;
        public BOLogin(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<PetitionResponse> Login(EpicorCredentials EpicorCredentials)
        {
            string strUrlMethod = strUrl + "/Login";
            try
            {
                string method = "Ice.BO.UserFileSvc/";
                string inst = "GetUserFile";
                string type = "returnObj";
                string entity = null;
                EpicorCredentials epicorCredentials = EpicorCredentials;

                var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);
                if (response != null)
                {
                    Ice.Tablesets.UserFileTableset userFileTableset = new Ice.Tablesets.UserFileTableset();
                    userFileTableset = (response as JObject).ToObject<Ice.Tablesets.UserFileTableset>();

                    if (userFileTableset.UserFile.Count > 0)
                        return Shared.Answer(true, "Credenciales Correctas!", strUrlMethod, strUrl, userFileTableset.UserFile[0].Name);
                }
                else
                    return Shared.Answer(false, "No fue posible realizar la consulta", strModule, strUrlMethod, null);

                return Shared.Answer(false, "Error al Consultar el Usuario.", strUrlMethod, strUrl, null);

            }
            catch (Exception ex)
            {
                return Shared.Answer(false, ex.Message, strModule, strUrlMethod, null);
            }
        }

    }
}
