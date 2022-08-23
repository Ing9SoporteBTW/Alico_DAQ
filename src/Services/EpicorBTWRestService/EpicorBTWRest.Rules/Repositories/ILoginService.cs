using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Repositories
{
    public interface ILoginService
    {
        Task<PetitionResponse> Login(EpicorCredentials epicorCredentials);
    }
}
