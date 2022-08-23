using SharedService.Models;
using SharedService.Responses;


namespace EpicorBTWRest.Rules.Repositories
{
    public interface IUD05Service
    {
        Task<PetitionResponse> GetNewUD05(string UD05, EpicorCredentials epicorCredentials);
    }
}
