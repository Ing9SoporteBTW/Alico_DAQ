using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Models;

namespace EpicorBTWRest.Rules.Functions
{
    public class EpicorRequests
    {
        ILogger log { get; set; }

        public EpicorRequests(ILogger _log)
        {
            log = _log;
        }

        public async Task<object> Get(string Method, string Inst, string Type, EpicorCredentials epicorCredentials)
        {
            ServicePointManager.ServerCertificateValidationCallback += (senderC, cert, chain, sslPolicyErrors) => true;

            var stopWatch = new Stopwatch();
            var client = new RestSharp.RestClient(epicorCredentials.EpicorAPI + Method + Inst);
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            request.AddHeader("authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", epicorCredentials.Username, epicorCredentials.Password))));
            
            stopWatch.Start();
            var response = await client.ExecuteGetAsync(request);
            stopWatch.Stop();

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("No se encontro el Api de epicor, url erronea.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Credenciales Erroneas");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = JsonConvert.DeserializeObject<ResponseErrorEpicor>(response.Content.ToString());
                return content.ErrorMessage;
            }

            object obj = new object();
            if (!string.IsNullOrEmpty(Type))
                obj = JObject.Parse(response.Content).SelectToken(Type, false);

            return obj;

            //if (response.StatusCode == HttpStatusCode.NotFound)
            //    return null;

            //return JObject.Parse(response.Content).SelectToken(Type, false);
        }
        public async Task<object> PostRest(string Method, string Inst, string Type, string entity, EpicorCredentials epicorCredentials)
        {
            ServicePointManager.ServerCertificateValidationCallback += (senderC, cert, chain, sslPolicyErrors) => true;

            var stopWatch = new Stopwatch();
            var client = new RestSharp.RestClient(epicorCredentials.EpicorAPI + Method + Inst);
            var request = new RestSharp.RestRequest(RestSharp.Method.POST);
            request.AddParameter("application/json", entity, RestSharp.ParameterType.RequestBody);
            request.AddHeader("authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", epicorCredentials.Username, epicorCredentials.Password))));
            request.AddHeader("content-type", "application/json");

            stopWatch.Start();
            var response = await client.ExecutePostAsync(request);
            stopWatch.Stop();

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("No se encontro el Api de epicor, url erronea.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Credenciales Erroneas");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = JsonConvert.DeserializeObject<ResponseErrorEpicor>(response.Content.ToString());
                return content.ErrorMessage;
            }

            object obj = new object();
            if (!string.IsNullOrEmpty(Type))
                obj = JObject.Parse(response.Content).SelectToken(Type, false);

            return obj;
        }
        public async Task<object> PatchRest(string Method, string Inst, string Type, string entity, EpicorCredentials epicorCredentials)
        {
            ServicePointManager.ServerCertificateValidationCallback += (senderC, cert, chain, sslPolicyErrors) => true;

            var stopWatch = new Stopwatch();
            var client = new RestSharp.RestClient(epicorCredentials.EpicorAPI + Method + Inst);
            var request = new RestSharp.RestRequest(RestSharp.Method.PATCH);
            request.AddParameter("application/json", entity, RestSharp.ParameterType.RequestBody);
            request.AddHeader("authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", epicorCredentials.Username, epicorCredentials.Password))));
            request.AddHeader("content-type", "application/json");

            stopWatch.Start();
            //var response = await client.ExecutePostAsync(request);
            var response = await client.ExecuteAsync(request);
            stopWatch.Stop();

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("No se encontro el Api de epicor, url erronea.");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Credenciales Erroneas");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = JsonConvert.DeserializeObject<ResponseErrorEpicor>(response.Content.ToString());
                return content.ErrorMessage;
            }

            object obj = new object();
            if (!string.IsNullOrEmpty(Type))
                obj = JObject.Parse(response.Content).SelectToken(Type, false);

            return obj;
        }
    }
}
