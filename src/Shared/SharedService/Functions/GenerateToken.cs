using Newtonsoft.Json.Linq;
using RestSharp;
using SharedService.Responses;

namespace SharedService.Functions
{
    public class GenerateToken
    {
        public async Task<PetitionResponse> Token(string url, string client_id, string secret_id, string scope)
        {
            var client = new RestClient($"{url}/connect/token");
            client.Timeout = -1;
            client.FollowRedirects = false;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", secret_id);
            request.AddParameter("scope", scope);
            IRestResponse response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
                throw new Exception($"Error en la petición {response.ErrorMessage} con código de error {response.StatusCode}");

            return new PetitionResponse
            {
                success = true,
                message = "Token generado con éxito!!!",
                result = JObject.Parse(response.Content)
            };
        }
    }
}
