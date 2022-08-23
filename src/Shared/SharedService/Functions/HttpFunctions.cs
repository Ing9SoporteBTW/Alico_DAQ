using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharedService.Responses;
using System.Net;
using System.Text;

namespace SharedService.Functions
{
    public class HttpFunctions
    {
        string token;
        string url;
        ILogger log;

        public HttpFunctions(ILogger _log, string _token, string _url)
        {
            log = _log;
            token = _token;
            url = _url;
        }
        public HttpFunctions(ILogger _log, string _url)
        {
            log = _log;
            url = _url;

            token = string.Empty;
        }

        public async Task<PetitionResponse> SendRequestPost(object item)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                http.BaseAddress = new Uri(url);
                log.LogDebug($"Url SendRequestPost: {url}");

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;
                var response = await http.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item).ToString(), Encoding.UTF8, "application/json"));
                var result = response.Content.ReadAsStringAsync().Result;
                JObject json = new JObject();
                json = JObject.Parse(result);

                if (result != null)
                {
                    PetitionResponse objPetitionResponse = JsonConvert.DeserializeObject<PetitionResponse>(json.ToString());
                    return objPetitionResponse;
                }
                else
                    return new PetitionResponse
                    {
                        success = false,
                        message = "No se pudo hacer el consumo",
                    };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    success = false,
                    message = $"Error durante el consumo {ex.Message}",
                    result = null,
                };
            }
        }

        public async Task<PetitionResponse> SendRequestPostAnonymous(object item)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(url);
                var response = await http.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item).ToString(), Encoding.UTF8, "application/json"));
                var result = response.Content.ReadAsStringAsync().Result;
                JObject json = new JObject();
                json = JObject.Parse(result);

                if (result != null)
                {
                    PetitionResponse objPetitionResponse = JsonConvert.DeserializeObject<PetitionResponse>(json.ToString());
                    return objPetitionResponse;
                }
                else
                    return new PetitionResponse
                    {
                        success = false,
                        message = "No se pudo hacer el consumo",
                    };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    success = false,
                    message = $"Error durante el consumo {ex.Message}",
                    result = null,
                };
            }
        }

        public async Task<PetitionResponse> SendRequestGetAnonymous()
        {
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(url);
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;
                var response = await http.GetAsync(url);
                var result = response.Content.ReadAsStringAsync().Result;
                JObject json = new JObject();
                json = JObject.Parse(result);

                if (result != null)
                {
                    PetitionResponse objPetitionResponse = JsonConvert.DeserializeObject<PetitionResponse>(json.ToString());
                    return objPetitionResponse;
                }
                else
                    return new PetitionResponse
                    {
                        success = false,
                        message = "No se pudo hacer el consumo",
                    };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    success = false,
                    message = $"Error durante el consumo {ex.Message}",
                    result = null,
                };
            }
        }

        public async Task<PetitionResponse> SendRequestGet()
        {
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Authorization
                    = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                http.BaseAddress = new Uri(url);
                log.LogDebug($"Url SendRequestGet: {url}");

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;
                var response = await http.GetAsync(url);
                var result = response.Content.ReadAsStringAsync().Result;
                JObject json = new JObject();
                json = JObject.Parse(result);

                if (result != null)
                {
                    PetitionResponse objPetitionResponse = JsonConvert.DeserializeObject<PetitionResponse>(json.ToString());
                    return objPetitionResponse;
                }
                else
                    return new PetitionResponse
                    {
                        success = false,
                        message = "No se pudo hacer el consumo",
                    };
            }
            catch (Exception ex)
            {
                return new PetitionResponse
                {
                    success = false,
                    message = $"Error durante el consumo {ex.Message}",
                    result = null,
                };
            }
        }

        public async Task<bool> SendRequestPostNoResult(object item)
        {
            bool result = false;
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                http.BaseAddress = new Uri(url);
                log.LogDebug($"Url SendRequestPost: {url}");

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;
                var response = await http.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item).ToString(), Encoding.UTF8, "application/json"));

                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex}");
            }
            return result;
        }

        public async Task<bool> SendRequestPostAnonymousNoResult(object item)
        {
            bool result = false;
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(url);

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;
                var response = await http.PostAsync(url, new StringContent(JsonConvert.SerializeObject(item).ToString(), Encoding.UTF8, "application/json"));

                if (response != null)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Error: {ex}");
            }
            return result;
        }

    }
}
