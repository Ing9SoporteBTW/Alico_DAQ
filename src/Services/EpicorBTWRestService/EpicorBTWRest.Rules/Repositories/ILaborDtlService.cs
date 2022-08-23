using SharedService.Models;
using SharedService.Responses;

namespace EpicorBTWRest.Rules.Repositories
{
    public interface ILaborDtlService
    {
        Task<PetitionResponse> GetNewLaborDtl(EpicorCredentials epicorCredentials);
    }
}
