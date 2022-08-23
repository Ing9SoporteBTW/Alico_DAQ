using EpicorBTWRest.DataAccess.Indirect;
using EpicorBTWRest.Rules.Functions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicorBTWRest.Rules.BusinessObjects
{
    public class BOIndirect
    {
        private const string strModule = "Indirect";
        private static string strUrl = $@"{SharedService.Functions.Helpers.APIPrefix}/{strModule}";

        public EpicorCredentials epicorCredentials { get; set; }
        public void Dispose()
        {
        }
        ILogger log { get; set; }
        EpicorRequests epicorRequests;

        public BOIndirect(ILogger _log)
        {
            log = _log;
            epicorRequests = new EpicorRequests(log);
        }

        public async Task<string> GetByID(string indirectCode, EpicorCredentials epicorCredentials)
        {
            
            GetByID getByID_input = new GetByID()
            {
                indirectCode = indirectCode,
            };

            string method = "Erp.BO.IndirectSvc/";
            string inst = "GetByID";
            string type = "returnObj";
            string entity = JsonConvert.SerializeObject(getByID_input);


            var response = await epicorRequests.PostRest(method, inst, type, entity, epicorCredentials);

            if (response == null) throw new Exception("GetByID_Indirect");


            string ds = (response as JObject).SelectToken("Indirect[0].ExpenseCode", false).ToObject<string>();

            if (ds == null) return "LOGISTIC";

            return ds;
        }

        public async Task<string> ObtenerInfoIndirecto (string codigoParo, EpicorCredentials epicorCredentials)
        {
            if (codigoParo == null)
                throw new Exception("No se entrego el parametro");

            string ExpenseCode = await GetByID(codigoParo, epicorCredentials);
            return ExpenseCode;

        }

    }
}
